using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
//*****************************
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using System.Drawing;

class ClassTrain : IDisposable
{
    #region değişkenler listeler
    FaceRecognizer recognizer;
  
    List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();//eğitilecek olan resimler için liste oluşturuldu.
    
    List<string> AdSoyadList = new List<string>(); //eğitilicek olan resimlerin kime ait olduğunu tutmak için oluşturduk.(******)
    List<int> KisiIdList = new List<int>(); //eğitilecek olan resimlerin Id bilgilerini tutmak için liste oluşturduk.(*******)
    int ContTrain, NumLabels;
    float YuzDistance = 0;//Distance değerini kullanabilmek için float tipinde değişken oluşturduk.(******)
    string AdSoyad_label;//Tahmin edilen ad soyad bilgisini tutmak için oluşturduk.(***********)

    int OzyuzEsikDeger = 2000; // öz yüz eşik değeri (*************)    

    string Error; //Aldığımız hataları yazdırmak için değişken oluşturduk.(******)
    bool _DizinKontrol = false; // "TrainedFaces" klasörünün olup olmadığını kontrol etmek için değişken oluşturdum.(****)

    public string TanimaTuru = "EMGU.CV.EigenFaceRecognizer"; //Varsayılan olarak eigenfacerecognizer atandı.(**************)

    #endregion

    // Temizleme işlemi
    public void Dispose()
    {
        recognizer = null;
        trainingImages = null;
        AdSoyadList = null;
        Error = null;
        GC.Collect();
    }

    //Uygulamanı çalıştığı dizine gider ve "TrainedFaces" klasörünün var olup olmadığına bakar. varsa true değerini döner.(**)
    //class çalıştığında ilk önce bu metot çalışır. Uygulamanın çalıştığı dizinde bulunan "TrainedFaces" klasörünü filepath olarak verdim.(**)
    //FnkEgitimVerileriYukle metodunu çalıştırarak eğitim dosyasını yükler ve _DizinKontrol değişkenine true veya false değerini atar.(**)
    public ClassTrain()
    {
        _DizinKontrol = FnkEgitimVerileriYukle(Application.StartupPath + "\\TrainedFaces");
    }

    
    //eğitim verisinin bulunduğu dizin konumunu kullanarak eğitim verilerini yükler.
    public ClassTrain(string Training_Folder)
    {
        _DizinKontrol = FnkEgitimVerileriYukle(Training_Folder);
    }

    
    //FnkEgitimVerileriYukle metodu çalıştıktan sonra Dizinkontrol değeri true veya false değerini alır. Değeri kullanabilmek için metot oluşturdum.(**)
    public bool DizinKontrol
    {
        get { return _DizinKontrol; }
    }


     //Uygulama ilk çalıştığında bu fonksiyon kullanılır. Daha sonra Form1'deki tanıma türünü değiştirdikçe çalışır.(**)
    //Belirtilen dizinin kontrolü yapılır ve dizinde xml dosyası varsa xml dosyalarını okuma ve eğitim yüklemelerini gerçekleştirir.(**)
    //Aynı zamanda yapılan algoritma eğitimine ait yüzlerin sayısal verilerini dat uzantılı olarak kaydeder.(**)
    private bool FnkEgitimVerileriYukle(string Folder_location)
    {
        if (File.Exists(Folder_location + "\\TrainedLabels.xml"))//xml dosyasının olup olmadığını kontrol ediyoruz.(**)
        {
            try
            {
                //yükleme işleminden önce eski verileri temizledim.(**)
                AdSoyadList.Clear();
                KisiIdList.Clear();
                trainingImages.Clear();

                //Okuma işlemi için filestream sınıfını kullandım.(**) 
                //"TrainedLabels.xml" dosyasında resimlerin kime ait olduğuna ait bilgiler tutulmakta.(**)
                FileStream FSKisiBilgileri = File.OpenRead(Folder_location + "\\TrainedLabels.xml");

                long filelength = FSKisiBilgileri.Length;//akışın uzunluğunu aldık.
                byte[] xmlBytes = new byte[filelength];
                FSKisiBilgileri.Read(xmlBytes, 0, (int)filelength);
                FSKisiBilgileri.Close();

                //xml dosyasındaki verileri her seferinde dosyaya erişek yapmak zaman kaybı olur.(**)
                //Bunun için memorystream ile verileri bellekte geçici olarak tutum.(**)
                MemoryStream xmlStream = new MemoryStream(xmlBytes);

                using (XmlReader xmlreader = XmlTextReader.Create(xmlStream))
                {
                    while (xmlreader.Read())
                    {
                        if (xmlreader.IsStartElement())
                        {
                            switch (xmlreader.Name)
                            {
                                case "NAME":
                                    if (xmlreader.Read())
                                    {
                                        //Her name elementine geldiğinde eleman sayısını sayarak kişiye Id verdim.(**)
                                        KisiIdList.Add(AdSoyadList.Count);
                                       //xml dosyasındaki  name elementideki ad soyad bilgilerini AdSoyadList listesine ekledik.(**)
                                       //Trim değerdeki boşlukları temizler.(**)
                                        AdSoyadList.Add(xmlreader.Value.Trim());
                                        NumLabels += 1;
                                    }
                                    break;
                                case "FILE": //elementin içinde kişiye ait olan yüz resimlerinin bilgisi yer alır.(**)
                                    if (xmlreader.Read())
                                    {                                        
                                        //elementteki isim bilgisine göre dosyadaki resme ulaşır ve "trainingImages" listesine resmi ekler.(**)
                                        trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "\\TrainedFaces\\" + xmlreader.Value.Trim()));
                                    }
                                    break;
                            }
                        }
                    }
                }
                ContTrain = NumLabels;
            
                //XML verilerine göre isimler ve yüz resimleri eşleştirilerek listeye eklendi.(**)
                //Şimdi listedeki veriler bizim seçtiğimiz tanıma algoritmasına göre eğitme işlemi yapılacak(**)
                if (trainingImages.ToArray().Length != 0)//eğitilecek yüzlerin bulunduğu liste boş değilse içeri girer.(**)
                {
                    string dosyaisim = "";//eğitim verilerini kaydetmek için hangi tanıma algoritmasını seçtiğini anlamak için değişken oluşturdum.(**) 
                    
                    //Tanıma algoritması varsayılan olarak Eigenfaces verdim.(**)
                    //Daha sonra bizim seçtiğimiz algoritma tipine göre aşağıda değişimler olacak.(**)
                    switch (TanimaTuru)
                    {
                        case ("EMGU.CV.LBPHFaceRecognizer"):
                            recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);//50
                            dosyaisim = "trainedLBPH.dat";
                            break;
                        case ("EMGU.CV.FisherFaceRecognizer"):
                            recognizer = new FisherFaceRecognizer(0, 3500);//4000
                            dosyaisim = "trainedFisherFace.dat";
                            break;
                        case ("EMGU.CV.EigenFaceRecognizer"):
                        default:
                            recognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
                            dosyaisim = "trainedEigenFaces.dat";
                            break;
                    }
                    //seçilen algoritmaya göre traininImages'deki resimler eğitilir.(**) 
                    //Train fonksiyonu parametre olarak resim ve resme ait Id etiketi alır(**)
                    recognizer.Train(trainingImages.ToArray(), KisiIdList.ToArray());

                    //getcurrent=belirtilen yol dizinin dizin bilgisini döndürür.(c:\ugur\dosya.txt  çıktı: c:\ugur olur.)(**)
                    string dataDirectory = Directory.GetCurrentDirectory() + "\\TrainedFaces";
                    recognizer.Save(dataDirectory + "\\train\\" + dosyaisim);// train klasörüne eğitim dosyasını kaydeder.(**)

                    //if döngüsünün içindeki işlemler başarılı sonuç verdiyse yükleme tamamlanmış olur bu yüzden true değerini döndürdüm.(**)
                    return true; 
                }
                else return false; // eğer yüzlerin bulunduğu yüz eğitim listesi boş ise if döngüsüne girmez ve else ile false değerini döndürdüm.(**)
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return false; 
            }
        }
        else return false;
    }


  
    //Tanıma yöntemini değiştirmek için seçtiğimiz butona tıkladığımızda seçilen yöntemin eğitilmesi için bu fonksiyon çalışır.(**)
    //FnkEgitimVerileriYukle fonksiyonunu çalıştırır.
    public bool Retrain()
    {
        return _DizinKontrol = FnkEgitimVerileriYukle(Application.StartupPath + "\\TrainedFaces");
    }

      

    
    public bool Retrain(string Training_Folder)
    {
        return _DizinKontrol = FnkEgitimVerileriYukle(Training_Folder);
    }
    


    //Yüz  tanıma işlemini gerçekleştirecek olan metot(**)
    public string Recognition(Image<Gray, byte> Input_image)
    {
        if (_DizinKontrol)//dizin kontrol değeri true ise tanıma işlemine geçilir
        {
            //Tanıma işlemini yapar. predict fonksiyonu parametre olarak tanınması istenen yüzü girdi olarak alır.(**)
            FaceRecognizer.PredictionResult ER = recognizer.Predict(Input_image);

            //eğer tanıma işleminin sonucu -1 çıkarsa kameradan algılanan kişinin tanınmadığı anlamına gelir.(**)
            if (ER.Label == -1)
            {
                AdSoyad_label = "TANINMADI";
                YuzDistance = 0;
                return AdSoyad_label; //eğer yüz tanınmadıysa "TANINMADI" şeklinde mesaj gönderilir.(**)
            }
            else
            {
                AdSoyad_label = AdSoyadList[ER.Label]; //eğer yüz tanındıysa tanınan kişinin ad soyad bilgisini aldı(**)
                YuzDistance = (float)ER.Distance; //tanınan yüzün değeri YuzDistance değişkenine atanır.(**)


               // if (Eigen_Thresh > -1) OzyuzEsikDeger = Eigen_Thresh;//yorum satırı yap(**)

                //eigenfaces algoritması kullanıyorsak eşik değerini kullanırız.(**)    
                //TanimaTuru değişkeni form ekranında seçtiğimiz tanıma yöntemine göre değişmekte. o yüzden public tanımlamıştım.(**) 
                switch (TanimaTuru)
                {
                    case ("EMGU.CV.EigenFaceRecognizer"):
                        //yüzün sayısal değeri eşik değerinden büyük ise ad soyad değeri döndürülür.(**)
                        if (YuzDistance > OzyuzEsikDeger) return AdSoyad_label;
                        else return "TANINMADI";
                    case ("EMGU.CV.LBPHFaceRecognizer"):
                    case ("EMGU.CV.FisherFaceRecognizer"):
                    default:
                        return AdSoyad_label;
                }             
            }
        }
        else return "";
    }
   


    // form ekranındaki textboxdan girdiğimiz eigenface için olan eşik değerini class'ta kullanabilmek için metot oluşturdum.
    public int SetEigenEsikDeger
    {
        set
        {
            OzyuzEsikDeger = value;
        }
    }



    // YuzDistance değerini okumak için fonksiyon oluşturduk.(**)
    public float GetYuzDistance { get { return YuzDistance; } }

    

    //Tanınan kişinin ad soyad bilgilerini döndürür.(**)
    public string GetAdSoyadLabel
    { get{return AdSoyad_label; }}
    
   
  

    // Hata mesajını yazdırmak için kullandık.(**)
    public string Get_Error{get { return Error; }}




    // Eğitilen resimlerin sayısal değerlerini kaydetmek için yazılan kodlar
    public void FnkEgitimDosyasiKaydet(string filename)
    {
        recognizer.Save(filename);//eğitim dosyasını belirtilen konuma kaydeder.

        //Etiket bilgilerini kaydetmek için aşağıdaki kodlar yazıldı.
        string filepath = Path.GetDirectoryName(filename);//belirtilen yol dizesinin yol bilgisini aldık.
        FileStream FSKisiBilgileri = File.OpenWrite(filepath + "/Labels.xml"); //xml dosyası yazmak için FileStream nesnesi oluşturduk.(********)
        using (XmlWriter writer = XmlWriter.Create(FSKisiBilgileri))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Labels_For_Recognizer_sequential");
            for (int i = 0; i < AdSoyadList.Count; i++)
            {
                writer.WriteStartElement("LABEL");
                writer.WriteElementString("POS", i.ToString());
                writer.WriteElementString("NAME", AdSoyadList[i]);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
        FSKisiBilgileri.Close();
    }

    //Eğer biz yeni bir eğitim dosyası yüklemek istersek aşağıdaki kod bloğu çalışır.
    // Belirtilen dosya yolundaki eğitim setini yükler.
    public void FnkEgitimDosyasiniYukle(string filename)// filename trainedfaces klasörünün yolu
    {        
        string dosyauzantisi = Path.GetExtension(filename);//belirtilen yolun uzantısını verir.
        
        switch (dosyauzantisi)//seçilen dosyanın uzantısına göre tanıma algoritması seçilir.
        {
            case (".LBPH"):
                TanimaTuru = "EMGU.CV.LBPHFaceRecognizer";
                recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);//50
                break;
            case (".FFR"):
                TanimaTuru = "EMGU.CV.FisherFaceRecognizer";
                recognizer = new FisherFaceRecognizer(0, 3500);//4000
                break;
            case (".EFR"):
                TanimaTuru = "EMGU.CV.EigenFaceRecognizer";
                recognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
                break;
        }

     
        recognizer.Load(filename);//seçilen dosya yüklenir.(******)
       
        //Şimdide Etiketleri yükleme işlemi gerçekleştirilir.
        string filepath = Path.GetDirectoryName(filename);//eğitim dosyasının bulunduğu dizini verir.(sadece dizin.)
        AdSoyadList.Clear();
        if (File.Exists(filepath + "/Labels.xml"))//belirtilen dizinde xml dosyasının olup olmadığının kontrolü yapılır.(*******)
        {
            FileStream FSKisiBilgileri = File.OpenRead(filepath + "/Labels.xml");
            long filelength = FSKisiBilgileri.Length;//dosyanın uzunluğunu aldık.
            byte[] xmlBytes = new byte[filelength];
            FSKisiBilgileri.Read(xmlBytes, 0, (int)filelength);
            FSKisiBilgileri.Close();

            //xml'deki verileri her seferinde dosyaya ulaşıp alamk yerine 
            //memorystream kullanarak bellekte tutarak uygulamanın performansını arttırmayı sağladım.(******)
            MemoryStream xmlStream = new MemoryStream(xmlBytes);

            using (XmlReader xmlreader = XmlTextReader.Create(xmlStream))
            {
                while (xmlreader.Read())
                {
                    if (xmlreader.IsStartElement())
                    {
                        switch (xmlreader.Name)
                        {
                            case "NAME":
                                if (xmlreader.Read())
                                {
                                    AdSoyadList.Add(xmlreader.Value.Trim());//xml dosyasından okuduğu isimleri AdSoyadList listesine ekliyoruz.
                                }
                                break;
                        }
                    }
                }
            }
            ContTrain = NumLabels;
        }
        _DizinKontrol = true;

    }
    



    

   

}

