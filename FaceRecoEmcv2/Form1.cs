using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//*******************
using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace FaceRecoEmcv2
{
    public partial class Form1 : Form
    {
        #region değişkenler ve tanımlamalar
        Image<Bgr, Byte> currentFrame; //webcamden alınan kamera görüntüsünü tutması için bgr renk uzayından image tipinde değişken tanımladık.
        Image<Gray, byte> result = null; //algılanan yüzü ve eğitme için kullanılacak olan yüzü tutmak için gri fortta Image değişkenleri oluşturduk.
        Image<Gray, byte> grayframe = null; //kameradan alınan anlık görüntüleri gri formata dönüştürünce atayacağımız Image değişkeni
        Capture _capture; //yakalama değişkeni
        //Haarcascade sınıfınıfından nesne oluşturdum. Bu sayede yüz algılama işlemini gerçekleştireceğiz. (**)
        public CascadeClassifier FaceCascade = new CascadeClassifier(Application.StartupPath + "/Cascades/haarcascade_frontalface_default.xml");
        //bulunan yüzün etrafını çizmek için oluşturduk.(**)
        //yüz algılarken yüzü çerçeve içine alma işleminde kullanacağız.(**)
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);
        #endregion
        //Classifier_Train'deki class'ta bulunan metotları kullanabilmek için nesne oluşturdum.(**)
        ClassTrain CsTrain = new ClassTrain();  

        public Form1()
        {
            InitializeComponent();                                   
            //CsTrain'deki DizinKontrol metodundan true false değeri döndürülerek true false değerine göre cevap yazdırılır.(**)
            //Kısaca eğitim verisinin yüklenip yüklenmediği kontrol edilir.(**)
            if (CsTrain.DizinKontrol) { message_bar.Text = "Eğitim verileri yüklendi."; }
            else { message_bar.Text = "Eğitim verisi bulunamadı. İlk önce yüz eğitimi gerçekleştirin."; }

            CameraCapture();//Kamerayı çalıştıran fonksiyonu çağırdık.(**)
            lblTanimaTur.Text = "Eigenfaces";//varsayılan olrak tanıma türünü eigenfaces verdim.(**)
        }

        //kameradan görüntü almak için kamerayı açma işlemi gerçekleştirir.
        public void CameraCapture()
        {
            _capture = new Capture(0);
            _capture.QueryFrame();
            //Idle uygulamanın atıl moda geçmesini temsil eder.(**)
            //Uygulamada klavye mouse hareketi olmadığı için arka tarafta iş yok gözükür ve uygulama atıl moda geçer.(**)
            //herhangi bir kontrol tarafından değil doğrudan windows tarafından tetiklenir.(**)
             Application.Idle += new EventHandler(GoruntuYakala_Parrellel);
            #region
            /*  
              if (parrellelToolStripMenuItem.Checked) { Application.Idle += new EventHandler(FrameGrabber_Parrellel); }
              else { Application.Idle += new EventHandler(FrameGrabber_Standard); }
              */
            #endregion
        }             
        //görüntü alma işlemini sonlandırır.
        private void StopCapture()
        {
             Application.Idle -= new EventHandler(GoruntuYakala_Parrellel);      
            if (_capture != null) { _capture.Dispose(); }
            #region
            /*
            if (parrellelToolStripMenuItem.Checked) { Application.Idle -= new EventHandler(FrameGrabber_Parrellel); }
            else { Application.Idle -= new EventHandler(FrameGrabber_Standard); }
            if (_capture != null) { _capture.Dispose(); }
            */
            #endregion
        }


        /*
        void FrameGrabber_Standard(object sender, EventArgs e)
        {
            //kameradan alınan anlık görüntü yeniden boyutlandırılır.(**)
            currentFrame = _capture.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //eğer görüntü alındıysa if yapısının içine girer.(**)
            if (currentFrame != null)
            {
                //Daha hızlı işlem yapabilmek için kameradan alınan anlık görüntü griye dönüştürülür.(**)
                gray_frame = currentFrame.Convert<Gray, Byte>();// kameradan alınan görüntüyü griye dönüştürdük.

                //Haarcascade'den türettiğimiz nesneden yüz algılama işlemini gerçekleştirdik.(**)
                Rectangle[] facesDetected = FaceCascade.DetectMultiScale(gray_frame, 1.2, 10, new Size(50, 50), Size.Empty);


                //Algılanan görüntünün çerçeveye alınacak bölgesi belirlenir.
                for (int i = 0; i < facesDetected.Length; i++)
                {
                    facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                    facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                    facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                    facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);
                    //Algılanan yüz kopyalanır ve gri resme dönüştürülür.
                    result = currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    //Griye dönüştürülen resme Histogram eşitleme işlemi yapılır.
                    result._EqualizeHist();
                    //belirlenen yüz kare içine alınarak çizilir.
                    currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                    if (CsTrain.DizinKontrol)//ilk önce dizinkontrol fonksiyonuna gider ve dizinin varlığı kontrol edilir.
                    {
                        string KisiAdSoyad = CsTrain.Recognition(result);//kameradan alınan anlık görüntü "recognise" sınıfına gönderilir.
                        //ve tanıma işleminden sonra tanınan kişinin ad soyad bilgisi döndürülür. 
                        int match_value = (int)CsTrain.GetYuzDistance; //classifier classından tanınan kişinin YuzDistance Değeri çekilir.

                        //algılanan ve tanınan yüz kare içine alınır.
                        currentFrame.Draw(KisiAdSoyad + " ", ref font, new Point(facesDetected[i].X - 2, facesDetected[i].Y - 2), new Bgr(Color.LightGreen));
                        //kameradan algılanan yüz adsoyad bilgisi ve yuzdistance değeri fonskiyona paremetre olarak verilir.
                        //Amacımız algılanan yüzü küçük boyutlarda ekranın sağ tarafında göstermek.
                        FnkKucukGoster(result, KisiAdSoyad, match_value);

                    }
                }
                //Sonucu görüntüler(**)
                imgKamera.Image = currentFrame.ToBitmap();
            }
        }
        */

        
        //yüz tanıma işlemini  gerçekleştiren metot. normal for döngüsü yerine parallel.for döngüsünü kullandım normal for'dan daha hızlı işlem yapar.
        void GoruntuYakala_Parrellel(object sender, EventArgs e)
        {
            //kameradan alınan anlık görüntü yeniden boyutlandırılır.(**)
            currentFrame = _capture.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            //eğer görüntü alındıysa if yapısının içine girer.(**)
            if (currentFrame != null)
            {
                //Daha hızlı işlem yapabilmek için kameradan alınan anlık görüntü griye dönüştürülür.(**)
                grayframe = currentFrame.Convert<Gray, Byte>();
                //Haarcascade'den türettiğimiz nesneden yüz algılama işlemini gerçekleştirdik.(**)
                Rectangle[] facesDetected = FaceCascade.DetectMultiScale(grayframe, 1.2, 10, new Size(50, 50), Size.Empty);
                //Algılanan görüntünün çerçeveye alınacak bölgesi belirlenir.(**)
                //Paralel.For normal for döngüsünden daha hızlı çalışır.(**)
                Parallel.For(0, facesDetected.Length, i =>
                {
                    try
                    {
                        facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                        facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                        facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                        facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);                     
                        //anlık görüntüden algılanan yüz resmini griye dönüştürür ve boyutlandırır.(100x100)(**) 
                        result = currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        result._EqualizeHist();//Histogram eşitleme(**)
                        //algılanan yüzü dikdörtgen içerisine alır.(**)
                        currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                        //cstrain classındaki dizinkontrol metodunun değerine bakar eğer eğitim verisi yüklüyse true değerini alır ve if içine girer(**)
                        if (CsTrain.DizinKontrol)
                        {
                            //Griye dönüştürülüp yeniden boyutlandırılan ve histogram eşitlemesi yapılmış anlık yüz görüntüsü(**)
                            //class'taki tanıma metoduna gönderilir ve sonuç bilgisi alınır.(**)
                            string KisiAdSoyad = CsTrain.Recognition(result);
                            int FaceValue = (int)CsTrain.GetYuzDistance;//yüzün sayısal değeri döndürülür.(**)
                            //Anlık görüntüden tanınan yüz çerçeveye alınır ve kim olduğu yazılır(**)
                            currentFrame.Draw(KisiAdSoyad + " ", ref font, new Point(facesDetected[i].X - 2, facesDetected[i].Y - 2), new Bgr(Color.LightGreen));
                            lblKisi.Text = KisiAdSoyad;
                            //Algılanan yüzleri form ekranının sağ tarafında distance değerleri ile küçük boyutlarda gösterme işlemi(**)
                            FnkKucukGoster(result, KisiAdSoyad, FaceValue);
                        }
                    }
                    catch{}
                });
                //Algılanan ve tanınan  yüzü göster.(**)
                imgKamera.Image = currentFrame.ToBitmap();
            }
        }
             

        
        private void BtnEigenface_Click(object sender, EventArgs e)
        {
            CsTrain.TanimaTuru = "EMGU.CV.EigenFaceRecognizer";//Cstrain nesnesi ile tanima turunu bilmesi için tanimaturunu gönderdik.(**)
            CsTrain.Retrain();//Tanıma yöntemini değiştirince eğitim verisini yeniden yüklemekye yarayacak metot(**)
            lblTanimaTur.Text = "Eigenface";
        }

        private void BtnFisherface_Click(object sender, EventArgs e)
        {
            CsTrain.TanimaTuru = "EMGU.CV.FisherFaceRecognizer";//Cstrain nesnesi ile tanima turunu bilmesi için tanimaturunu gönderdik.(**)
            CsTrain.Retrain();//Tanıma yöntemini değiştirince eğitim verisini yeniden yüklemekye yarayacak metot(**)
            lblTanimaTur.Text = "Fisherface";
        }

        private void BtnLBPH_Click(object sender, EventArgs e)
        {
            CsTrain.TanimaTuru = "EMGU.CV.LBPHFaceRecognizer";//Cstrain nesnesi ile tanima turunu bilmesi için tanimaturunu gönderdik.(**)
            CsTrain.Retrain();//Tanıma yöntemini değiştirince eğitim verisini yeniden yüklemekye yarayacak metot(**)
            lblTanimaTur.Text = "LBPH";
        }


        //Yeni yüz resmi eklemek istediğimiz zaman btnkisiekle butonu yüz ekleme formunu açar(**)
        private void BtnKisiEkle_Click(object sender, EventArgs e)
        {

            StopCapture();//Kamerayı çalışmayı durdurur.(**)            
            FrmKisiEkle TF = new FrmKisiEkle(this);//FrmKisiEkle formunu açar.(**)
            TF.Show();
        }

        //Eigenfaces yönteminde eşik değerini değiştirebilmek için txtboxın changed özelliği ile her değer değiştiğinde aşağıdaki işlem gerçekleşir.(**)
        private void Eigne_threshold_txtbx_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CsTrain.SetEigenEsikDeger = Math.Abs(Convert.ToInt32(TxtEigenEsikDegeri.Text));
                message_bar.Text = "Eigen Eşik değeri";
            }
            catch
            {
                message_bar.Text = "eşik değerinde int değerler kullanın";
            }
        }



        //Eğitme işlemini yeniden yükler.
        public void retrain()
        {
            CsTrain = new ClassTrain();
            if (CsTrain.DizinKontrol) { message_bar.Text = "Eğitim verileri yüklendi."; }
            else { message_bar.Text = "Eğitim verisi bulunamadı, ilk önce eğitim işlemini gerçekleştirin. "; }
        }

        //panelde yüz resimlerini göstermek için tanımladığım değişkenler(**)
        int faces_count = 0;
        int faces_panel_Y = 0;
        int faces_panel_X = 0;

        void FnkPanelTemizle()
        {
            this.Faces_Found_Panel.Controls.Clear();
            faces_count = 0;
            faces_panel_Y = 0;
            faces_panel_X = 0;
        }

        //Anlık görüntüden bulunan yüzleri distance kişi bilgileriyle küçük boyutlarda ekranda göstermek için(**)
        void FnkKucukGoster(Image<Gray, Byte> imgCaptureFace, string KisiAdSoyad, int FaceValue)
        {
            PictureBox PI = new PictureBox();
            PI.Location = new Point(faces_panel_X, faces_panel_Y);
            PI.Height = 80;
            PI.Width = 80;
            PI.SizeMode = PictureBoxSizeMode.StretchImage;
            PI.Image = imgCaptureFace.ToBitmap();
            Label LB = new Label();
            LB.Text = KisiAdSoyad + " " + FaceValue.ToString();
            LB.Location = new Point(faces_panel_X, faces_panel_Y + 80);
            //LB.Width = 80;
            LB.Height = 15;

            this.Faces_Found_Panel.Controls.Add(PI);
            this.Faces_Found_Panel.Controls.Add(LB);
            faces_count++;
            if (faces_count == 2)
            {
                faces_panel_X = 0;
                faces_panel_Y += 100;
                faces_count = 0;
            }
            else faces_panel_X += 85;

            if (Faces_Found_Panel.Controls.Count > 10)
            {
                FnkPanelTemizle();
            }

        }




        private void CikisMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();//bellekte program ile ilgili tutulan herşeyi siler. temizler(**)
        }



        //****************************************************BUNLARIN OLMASINDA KARARSIZIM*******************
        //eğitilmiş yüzlerin sayısal verilerini dosya formatına dökerek kaydetme işlemini yapar. 
        //Aynı zamanda xml dosyasıda oluşturarak kişi bilgilerini de verir.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Eğitim dosyası kaydetmek için çalışan kodlar.
            SaveFileDialog SF = new SaveFileDialog();
            //As there is no identification in files to recogniser type we will set the extension ofthe file to tell us
            switch (CsTrain.TanimaTuru)
            {
                case ("EMGU.CV.LBPHFaceRecognizer"):
                    SF.Filter = "LBPHFaceRecognizer File (*.LBPH)|*.LBPH";
                    break;
                case ("EMGU.CV.FisherFaceRecognizer"):
                    SF.Filter = "FisherFaceRecognizer File (*.FFR)|*.FFR";
                    break;
                case ("EMGU.CV.EigenFaceRecognizer"):
                    SF.Filter = "EigenFaceRecognizer File (*.EFR)|*.EFR";
                    break;
            }
            if (SF.ShowDialog() == DialogResult.OK)
            {
                CsTrain.FnkEgitimDosyasiKaydet(SF.FileName);
            }
        }

       

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            //Seçilen eğitim dosyasını yükler.
            OpenFileDialog OF = new OpenFileDialog();
            //Gözükecek dosya türlerini  belirttik.
            OF.Filter = "EigenFaceRecognizer File (*.EFR)|*.EFR|LBPHFaceRecognizer File (*.LBPH)|*.LBPH|FisherFaceRecognizer File (*.FFR)|*.FFR";
            if (OF.ShowDialog() == DialogResult.OK)
            {
                CsTrain.FnkEgitimDosyasiniYukle(OF.FileName);//Dosya ismini gönderdik.
            }
            
        }

       

                   

       

     

    }
}
