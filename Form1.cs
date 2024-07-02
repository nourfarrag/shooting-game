using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIGGame
{
    public class CImagActor
    {
        public int X, Y;
        public Bitmap img;
        public int Speed;
        public bool isLife = true;
        public int freverse = 0;
        public int health = 3;
    }

    public class CMultiImageActor
    {
        public int X, Y;
        public List<Bitmap> imgs;
        public int iFrame;
        public int xDir = 0;
        public int health = 3;

    }
    public class CAdvImag
    {
        public Bitmap img;
        public Rectangle rcDst;
        public Rectangle rcSrc;
        public int health = 3;
    }
    public class CActor
    {
        public int X, Y, W, H;
        public Color clr;
        public int health = 3;
    }
    public partial class Form1 : Form
    {


        Bitmap off;
        Timer tt = new Timer();
        List<CImagActor> lhooktrap = new List<CImagActor>();
        List<CMultiImageActor> LHero = new List<CMultiImageActor>();
        List<CMultiImageActor> lshieldenemy = new List<CMultiImageActor>();
        List<CMultiImageActor> LHeroRight = new List<CMultiImageActor>();
        List<CMultiImageActor> LElevator = new List<CMultiImageActor>();
        List<CMultiImageActor> LHeroLeft = new List<CMultiImageActor>();
        List<CMultiImageActor> LlaserEnemy = new List<CMultiImageActor>();
        List<CActor> lammocounter = new List<CActor>();
        List<CImagActor> lammo = new List<CImagActor>();
        List<CMultiImageActor> LHeroJump = new List<CMultiImageActor>();
        List<CAdvImag> LMap1 = new List<CAdvImag>();
        List<CAdvImag> LMap2 = new List<CAdvImag>();
        List<CImagActor> Lbullet = new List<CImagActor>();
        List<CAdvImag> LTile = new List<CAdvImag>();
        List<CAdvImag> LLadder = new List<CAdvImag>();
        List<CAdvImag> LLadderR2 = new List<CAdvImag>();

        int health = 3;
        int hookwidth = 0;
        int flagCreateExp = 0;
        int oncef = 0;
        int flagisinelevator;
        int ctTick = 0;
        int move = 0;
        int once = 0;
        int oncefire = 0;
        int flagshow = 0;
        int XScroll = 0;
        int increaselaserW = 0;
        int YScroll = 0;
        int fjump = 0;
        int jumpphase = 0;
        int jumponce = 0;
        int cxmargin = 1;
        int xmargin = 0;
        int cymargin = 1;
        int flagnearladder = 0;
        int flagnearTile = 0;
        int ct = 0;
        int fhooktrap = 0;
        int flongjump = 0;
        int newcreate = 0;
        int longjumponce = 0;
        int longjumpphase = 0;
        int ctonce = 0;
        int ct2once = 0;
        bool isMap1Active = true;
        int oncelaser = 0;//Y
        int cthealthenemy = 0;
        int cthealthero = 0;
        int elevatorisup = 0;
        int flagenter = 0;
        int fhookdir = 0;
        int cxmarginR2 = 1;
        int cyR2margin = 1;
        int cxmarginSpikeR2 = 1;
        int fswitchbomb = 0;//Y
        int value = 30;
        int fswitchrobot = 0;//Y
        List<CAdvImag> LTileR2 = new List<CAdvImag>();
        List<CAdvImag> LSpikeR2 = new List<CAdvImag>();
        List<CMultiImageActor> LBombEnemy = new List<CMultiImageActor>();
        List<CMultiImageActor> LBombEXP = new List<CMultiImageActor>();
        List<CMultiImageActor> Llasers = new List<CMultiImageActor>();
        List<CActor> lhealthsystem = new List<CActor>();

        public Form1()
        {
            this.Load += Form1_Load;
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            tt.Tick += Tt_Tick;
            tt.Interval = 100;
            tt.Start();

        }


        private void Tt_Tick(object sender, EventArgs e)
        {
            CreateBombDetect();
            CheckLadderR2();
            CheckHero();
            CheckTileR2();
            if (ctTick % 200 == 0)
            {
                Random rr = new Random();
                if (ctTick % rr.Next(150, 250) == 0)
                {
                    createammo();



                }
            }
            ctTick++;
            if (flagCreateExp == 1)
            {
                AnimateExp();

            }
            if (XScroll > 850)
            {
                if (newcreate == 0)
                {
                    CreateTileR2();
                    CreateSpikeR2();
                    CreateBombEnemy();
                    CreateLadderR2();
                    CreateLaserEnemy();
                    CheckHero();


                }
                newcreate = 1;
            }



            if (XScroll < 700)
            {
                CheckElevator();
                MoveElevator();


                CheckLadder();

                CheckTile();

            }

            if (XScroll > 850)
            {
                if (newcreate == 0)
                {
                    CreateTileR2();

                }
                newcreate = 1;
            }


            animateall();
            if (LHero.Count > 0 && LlaserEnemy.Count > 0)
            {
                this.Text = " frames of robot" + LlaserEnemy[0].iFrame + " XSCROLL: " + XScroll + "    RightHero: " + LHeroRight.Count + "Lefthero: " + LHeroLeft.Count + "Count" + ct + "lheroy" + LHero[0].Y + "lherox: " + LHero[0].X + " nearladder " + flagnearladder + "flagneartile" + flagnearTile;
            }
            if (fjump == 1)
            {

                jump();

            }
            if (flongjump == 1)
            {

                longjump();

            }
            Gravity();
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            CheckSpikeStrap();
            CreateBombDetect();
            CheckHero();
            if (XScroll > 850)
            {
                if (newcreate == 0)
                {
                    CreateTileR2();
                    CreateSpikeR2();
                    CreateBombEnemy();
                    CreateLadderR2();
                    CreateLaserEnemy();
                    CheckHero();

                }
                newcreate = 1;
            }
            animateall();
            if (LHero.Count > 0)
            {
                switch (e.KeyCode)
                {

                    case Keys.Left:
                        oncefire = 0;
                        LHero[0].iFrame = 1;
                        LHero[0].xDir = -1;

                        break;
                    case Keys.Right:
                        oncefire = 0;
                        LHero[0].iFrame = 0;
                        LHero[0].xDir = 1;


                        break;
                    case Keys.E:
                        flagenter = 1;
                        break;
                    case Keys.F:
                        if (value > 0)
                        {
                            value--;
                            if (fhookdir != 1)
                            {
                                if (LHero[0].xDir == 1)
                                {
                                    createbullet();
                                    for (int i = 0; i < Lbullet.Count; i++)
                                    {
                                        Lbullet[i].X += 5;
                                    }
                                    if (oncefire == 0)
                                    {
                                        LHero[0].iFrame = 4;
                                        oncefire = 1;
                                    }

                                    if (LHero[0].iFrame < 11)
                                    {

                                        LHero[0].iFrame++;

                                    }
                                    else
                                    {
                                        LHero[0].iFrame = 2;
                                    }


                                }

                                if (LHero[0].xDir == -1)
                                {
                                    createbullet();


                                    if (oncefire == 0)
                                    {
                                        LHero[0].iFrame = 11;
                                        oncefire = 1;
                                    }

                                    if (LHero[0].iFrame < 21)
                                    {

                                        LHero[0].iFrame++;

                                    }
                                    else
                                    {
                                        LHero[0].iFrame = 12;
                                    }
                                }
                            }
                        }

                        break;
                    case Keys.J:
                        fjump = 1;
                        break;

                    case Keys.Up:
                        if (flagnearladder == 1)
                        {
                            LHero[0].Y -= 10;




                        }
                        break;
                    case Keys.Down:

                        if (flagnearladder == 1)
                        {
                            LHero[0].Y += 10;

                        }
                        break;
                    case Keys.L:
                        flongjump = 1;
                        break;
                    case Keys.A:
                        collectammo();
                        break;
                }

                if (e.KeyCode == Keys.Space && fhookdir != 1)
                {

                    if (LHero[0].xDir == 1)
                    {
                        for (int i = 0; i < LLadderR2.Count; i++)
                        {
                            LLadderR2[i].rcDst.X -= 12;
                        }
                        for (int i = 0; i < lammo.Count; i++)
                        {
                            lammo[i].X -= 12;
                        }
                        if (once == 0 && LHero.Count > 0)
                        {
                            CreateHeroRight();
                            once = 1;
                        }
                        LHeroRight[0].X += 3;
                        LHero[0].X += 3;
                        XScroll += 6;
                        ///////////region2 scroll
                        ///
                        for (int i = 0; i < LlaserEnemy.Count; i++)
                        {
                            LlaserEnemy[0].X -= 8;
                        }


                        for (int i = 0; i < LSpikeR2.Count; i++)
                        {
                            LSpikeR2[i].rcDst.X -= 10;

                        }
                        for (int i = 0; i < LTileR2.Count; i++)
                        {

                            LTileR2[i].rcDst.X -= 10;
                        }

                        for (int i = 0; i < LTile.Count; i++)
                        {
                            LTile[i].rcDst.X -= 10;
                        }
                        for (int i = 0; i < LBombEnemy.Count; i++)
                        {
                            LBombEnemy[i].X -= 10;
                        }
                        LElevator[0].X -= 10;
                        if (lshieldenemy.Count > 0)
                        {
                            lshieldenemy[0].X -= 10;
                        }
                        if (lhooktrap.Count > 0)
                        {
                            lhooktrap[0].X -= 10;
                        }

                        LHeroRight[0].iFrame = (LHeroRight[0].iFrame + 1) % 5;
                        flagshow = 1;

                    }
                    if (LHero[0].xDir == -1)
                    {
                        for (int i = 0; i < LLadderR2.Count; i++)
                        {
                            LLadderR2[i].rcDst.X += 12;
                        }
                        for (int i = 0; i < lammo.Count; i++)
                        {
                            lammo[i].X += 12;
                        }
                        if (once == 0 && LHero.Count > 0)
                        {
                            CreateLeftHero();
                            once = 1;
                        }
                        LHeroLeft[0].X -= 3;
                        LHero[0].X -= 4;

                        XScroll -= 6;
                        for (int i = 0; i < LSpikeR2.Count; i++)
                        {
                            LSpikeR2[i].rcDst.X += 10;

                        }
                        for (int i = 0; i < LTileR2.Count; i++)
                        {

                            LTileR2[i].rcDst.X += 10;
                        }

                        for (int i = 0; i < LTile.Count; i++)
                        {
                            LTile[i].rcDst.X += 6;
                        }
                        LElevator[0].X += 6;
                        if (lshieldenemy.Count > 0)
                        {
                            lshieldenemy[0].X += 6;
                        }
                        for (int i = 0; i < LBombEnemy.Count; i++)
                        {
                            LBombEnemy[i].X += 10;
                        }
                        if (lhooktrap.Count > 0)
                        {
                            lhooktrap[0].X += 10;
                        }


                        for (int i = 0; i < LTile.Count; i++)
                        {
                            LTile[i].rcDst.X += 6;
                        }
                        LElevator[0].X += 6;
                        if (lshieldenemy.Count > 0)
                        {
                            lshieldenemy[0].X += 6;
                        }
                        if (LHeroLeft[0].iFrame > 0)
                        {
                            LHeroLeft[0].iFrame--;

                        }
                        else
                        {
                            LHeroLeft[0].iFrame = 6;
                        }
                        flagshow = 1;

                    }


                }
                else
                {

                    flagshow = 0;
                    if (LHeroRight.Count > 0)
                    {
                        LHeroRight.RemoveAt(0);
                    }
                    if (LHeroLeft.Count > 0)
                    {
                        LHeroLeft.RemoveAt(0);
                    }

                    once = 0;
                }

                LMap1[0].rcSrc.X = XScroll;
                DrawDubb(this.CreateGraphics());
            }

        }

        void CheckElevator()
        {
            if (flagenter == 1)
            {
                if (LHero[0].X > LElevator[0].X && LHero[0].X < LElevator[0].X + LElevator[0].imgs[0].Width - 150)
                {

                    LElevator[0].iFrame = 1;

                    flagisinelevator = 1;
                }
                else
                {

                    LElevator[0].iFrame = 0;
                }
            }



        }
        void AnimateExp()
        {

            if (LBombEXP.Count > 0)
            {
                if (LBombEXP[0].iFrame < 5)
                {
                    LBombEXP[0].iFrame++;
                }
                else
                {
                    LBombEXP.RemoveAt(0);
                    LBombEnemy.RemoveAt(0);
                    flagCreateExp = 0;
                    tt.Stop();

                }



            }




        }
        void MoveElevator()
        {
            if (flagisinelevator == 1 && elevatorisup == 0)
            {
                if (LElevator[0].Y > LTile[0].rcDst.Y - 250)
                {
                    LElevator[0].Y -= 10;
                    LHero[0].Y -= 10;
                    if (LHeroRight.Count > 0)
                    {
                        LHeroRight[0].Y -= 10;
                    }

                    if (LHeroLeft.Count > 0)
                    {
                        LHeroLeft[0].Y -= 10;
                    }

                    flagisinelevator = 1;
                }
                else
                {
                    LHero[0].X += 150;
                    if (LHeroRight.Count > 0)
                    {
                        LHeroRight[0].X += 150;
                    }

                    if (LHeroLeft.Count > 0)
                    {
                        LHeroLeft[0].X += 150;
                    }
                    flagnearTile = 1;
                    flagisinelevator = 0;

                    elevatorisup = 1;
                    LElevator[0].iFrame = 0;
                    flagenter = 0;
                }

            }
            if (elevatorisup == 1 && flagisinelevator == 1)
            {
                if (LElevator[0].Y < 400)
                {
                    LElevator[0].Y += 10;
                    LHero[0].Y += 10;
                    if (LHeroRight.Count > 0)
                    {
                        LHeroRight[0].Y += 10;
                    }

                    if (LHeroLeft.Count > 0)
                    {
                        LHeroLeft[0].Y += 10;
                    }

                    flagisinelevator = 1;
                }
                else
                {
                    flagisinelevator = 0;
                    elevatorisup = 0;
                    LHero[0].X -= 150;
                    flagenter = 0;
                    LElevator[0].iFrame = 0;
                }
            }





        }
        void CreateLaserEnemy()
        {
            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            for (int i = 0; i < 12; i++)
            {
                Bitmap img = new Bitmap("R" + (i + 1) + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            pnn.X = 1200;
            pnn.Y = 100;
            LlaserEnemy.Add(pnn);





        }
        void CreateLazer()
        {

            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            for (int i = 0; i < 12; i++)
            {
                Bitmap img = new Bitmap("z" + (i + 1) + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            pnn.X = 590 + 200;


            pnn.Y = 150;

            Llasers.Add(pnn);
        }
        void CheckHero()
        {

            if (LlaserEnemy.Count > 0)
            {
                if (LHero[0].X >= 580)
                {

                    if (oncelaser == 0)
                    {
                        CreateLazer();
                        oncelaser++;
                    }


                    if (Llasers[0].X + Llasers[0].imgs[0].Width >= LHero[0].X && Llasers[0].X <= LHero[0].X)
                    {
                        tt.Stop();
                    }
                    if (LlaserEnemy[0].iFrame < 12 && fswitchrobot == 0)
                    {
                        LlaserEnemy[0].iFrame++;
                        Llasers[0].iFrame++;
                        Llasers[0].X -= 5;
                        increaselaserW += 5;
                    }
                    else
                    {
                        fswitchrobot = 1;
                    }

                    if (LlaserEnemy[0].iFrame > 0 && fswitchrobot == 1)
                    {
                        LlaserEnemy[0].iFrame--;

                        Llasers[0].iFrame--;
                        Llasers[0].X -= 5;
                        increaselaserW += 5;

                    }
                    else
                    {
                        fswitchrobot = 0;
                    }

                }




            }




        }
        void createbullet()
        {
            CImagActor pnn = new CImagActor();
            pnn.img = new Bitmap("bullet2.png");

            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
            if (LHero[0].xDir == 1)
            {
                pnn.X = LHero[0].X + LHero[0].imgs[0].Width + 43;


            }
            else
            {
                pnn.X = LHero[0].X - 10;
            }
            pnn.Y = LHero[0].Y + 82;
            Lbullet.Add(pnn);
        }
        void CreateHero()
        {


            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();

            Bitmap img = new Bitmap("right.png");


            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.imgs.Add(img);


            img = new Bitmap("left.png");
            img.MakeTransparent(img.GetPixel(10, 10));
            pnn.imgs.Add(img);


            for (int i = 0; i < 10; i++)
            {
                img = new Bitmap("FR" + (i + 1) + ".png");


                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            for (int i = 0; i < 10; i++)
            {
                img = new Bitmap("FL" + (i + 1) + ".png");


                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);
            }
            pnn.X = 100;
            pnn.Y = 500;






            LHero.Add(pnn);







        }
        void longjump()
        {
            if (fhookdir != 1)
            {
                once = 1;
                if (longjumpphase == 0)
                {

                    flagshow = 1;

                    if (longjumponce == 0)
                    {
                        CreateHeroJump();
                        longjumponce = 1;
                    }
                    LHeroJump[0].iFrame = (LHeroJump[0].iFrame + 1) % 3;
                    ct++;
                    if (LHero[0].Y > 420)
                    {
                        if (LHero[0].xDir == 1)
                        {
                            LHeroJump[0].X += 5;
                            LHero[0].X += 5;
                            XScroll += 4;
                            for (int i = 0; i < LTile.Count; i++)
                            {
                                LTile[i].rcDst.X -= 4;
                            }
                            if (lshieldenemy.Count > 0)
                            {
                                lshieldenemy[0].X -= 4;
                            }
                            LElevator[0].X -= 4;
                            lhooktrap[0].X -= 4;
                            for (int i = 0; i < lammo.Count; i++)
                            {
                                lammo[i].X -= 4;
                            }



                        }
                        else
                        {
                            LHeroJump[0].X -= 10;
                            LHero[0].X -= 10;
                            XScroll -= 4;
                            for (int i = 0; i < LTile.Count; i++)
                            {
                                LTile[i].rcDst.X += 4;
                            }
                            if (lshieldenemy.Count > 0)
                            {
                                lshieldenemy[0].X += 4;
                            }
                        }
                        LHeroJump[0].iFrame++;

                        LHeroJump[0].Y -= 10;
                        LHero[0].Y -= 10;

                    }
                    else
                    {
                        longjumpphase = 1;

                    }
                }

                if (longjumpphase == 1)
                {
                    LHeroJump[0].iFrame = (LHeroJump[0].iFrame + 1) % 3;
                    if (LHero[0].Y < 500)
                    {

                        if (LHero[0].xDir == 1)
                        {
                            LHeroJump[0].X += 10;
                            LHero[0].X += 10;
                            XScroll += 4;
                            for (int i = 0; i < LTile.Count; i++)
                            {
                                LTile[i].rcDst.X -= 4;
                            }
                            if (lshieldenemy.Count > 0)
                            {
                                lshieldenemy[0].X -= 4;
                            }



                            LElevator[0].X -= 4;
                            lhooktrap[0].X -= 4;
                            for (int i = 0; i < lammo.Count; i++)
                            {
                                lammo[i].X -= 4;
                            }
                        }
                        else
                        {
                            LHeroJump[0].X -= 10;
                            LHero[0].X -= 10;
                            XScroll -= 4;
                            for (int i = 0; i < LTile.Count; i++)
                            {
                                LTile[i].rcDst.X += 4;
                            }
                            if (lshieldenemy.Count > 0)
                            {
                                lshieldenemy[0].X += 4;
                            }

                            LElevator[0].X += 4;
                            lhooktrap[0].X += 4;
                            for (int i = 0; i < lammo.Count; i++)
                            {
                                lammo[i].X -= 4;
                            }
                        }

                        LHeroJump[0].Y += 10;
                        LHero[0].Y += 10;

                    }
                    else
                    {
                        flongjump = 0;
                        longjumpphase = 0;
                        longjumponce = 0;
                        flagshow = 0;
                        once = 0;
                        LHeroJump.RemoveAt(0);
                    }
                }
                LMap1[0].rcSrc.X = XScroll;

            }
        }
        void CreateMap1()
        {
            CAdvImag pnn = new CAdvImag();
            pnn.img = new Bitmap("image (2).png");
            pnn.rcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            pnn.rcSrc = new Rectangle(XScroll, 0, this.ClientSize.Width / 2, pnn.img.Height);
            LMap1.Add(pnn);
        }
        void CreateMap2()
        {

            CAdvImag pnn = new CAdvImag();
            pnn.img = new Bitmap("2ndlvl.jpg");
            pnn.rcDst = new Rectangle(10000, 0, this.ClientSize.Width, this.ClientSize.Height);
            pnn.rcSrc = new Rectangle(0, 0, this.ClientSize.Width, pnn.img.Height);

            LMap2.Add(pnn);
        }
        void CreateHeroRight()
        {

            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            for (int i = 0; i < 5; i++)
            {
                Bitmap img = new Bitmap((i + 1) + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            pnn.X = LHero[0].X;
            pnn.Y = LHero[0].Y;

            LHeroRight.Add(pnn);





        }
        void CheckLadderR2()
        {
            if (LHero.Count > 0 && LLadderR2.Count > 0)
            {
                flagnearladder = 0;
                for (int i = 0; i < LLadderR2.Count; i++)
                {
                    if (LHero[0].X + 120 > LLadderR2[i].rcDst.X && LHero[0].X + 120 < LLadderR2[0].rcDst.X + LLadderR2[0].rcDst.Width &&
                        LHero[0].Y > 70)
                    {
                        flagnearladder = 1;

                    }
                    else
                    {
                        flagnearladder = 0;

                    }

                }
            }
        }
        void CreateHeroJump()
        {


            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            for (int i = 13; i < 17; i++)
            {
                Bitmap img = new Bitmap("jump_" + (i + 1) + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            pnn.X = LHero[0].X;
            pnn.Y = LHero[0].Y;

            LHeroJump.Add(pnn);





        }
        void CreateLeftHero()
        {



            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            for (int i = 0; i < 7; i++)
            {
                Bitmap img = new Bitmap("1" + (i + 1) + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            pnn.X = LHero[0].X;
            pnn.Y = LHero[0].Y;
            pnn.iFrame = 7;
            LHeroLeft.Add(pnn);






        }
        void CreateTile()
        {

            CAdvImag pnn;
            for (int i = 0; i < 9; i++)
            {
                pnn = new CAdvImag();
                pnn.img = new Bitmap("Tile.png");
                pnn.rcDst = new Rectangle(400 + cxmargin, 250, 100, 90);
                pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);

                LTile.Add(pnn);
                cxmargin += 100;

            }



        }
        void CreateTileR2()
        {

            CAdvImag pnn;
            for (int i = 0; i < 7; i++)
            {
                pnn = new CAdvImag();
                pnn.img = new Bitmap("Tile (12).png");
                pnn.rcDst = new Rectangle(0 + cxmarginR2, 250, 100, 50);
                pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);

                LTileR2.Add(pnn);
                cxmarginR2 += 100;

            }

        }


        void CreateLadder()
        {

            CAdvImag pnn;

            for (int i = 0; i < 7; i++)
            {
                pnn = new CAdvImag();
                pnn.img = new Bitmap("Ladde.png");
                pnn.rcDst = new Rectangle(1230, 235 + cymargin, 200, 90);
                pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                cymargin += 70;
                LTile.Add(pnn);
                LLadder.Add(pnn);
            }




        }
        void CreateLadderR2()
        {

            CAdvImag pnn;

            for (int i = 0; i < 7; i++)
            {
                pnn = new CAdvImag();
                pnn.img = new Bitmap("Ladde.png");
                pnn.rcDst = new Rectangle(600, 235 + cyR2margin, 200, 90);
                pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                cyR2margin += 70;
                LLadderR2.Add(pnn);
            }




        }


        void CheckLadder()
        {
            if (LHero.Count > 0)
            {
                flagnearladder = 0;
                for (int i = 0; i < LLadder.Count; i++)
                {
                    if (LHero[0].X + 120 > LLadder[i].rcDst.X && LHero[0].X + 120 < LLadder[0].rcDst.X + LLadder[0].rcDst.Width &&
                        LHero[0].Y > 70)
                    {
                        flagnearladder = 1;

                    }
                    else
                    {
                        flagnearladder = 0;

                    }

                }
            }
        }
        void createhooktrap()
        {
            CImagActor pnn = new CImagActor();
            pnn.X = 950;
            pnn.Y = LTile[0].rcDst.Y - 80;
            Bitmap img = new Bitmap("hooktrap.png");
            pnn.img = img;
            img.MakeTransparent(img.GetPixel(0, 0));
            lhooktrap.Add(pnn);
        }
        void CheckTile()
        {
            if (LHero.Count > 0)
            {


                for (int i = 0; i < LTile.Count; i++)
                {
                    if (LHero[0].X > LTile[0].rcDst.X - 250 &&
                        LHero[0].X + LHero[0].imgs[0].Width < LTile[LTile.Count - 1].rcDst.X + LTile[LTile.Count - 1].rcDst.Width &&
                        LHero[0].Y + LHero[0].imgs[0].Height - 20 > LTile[i].rcDst.Y)
                    {
                        flagnearTile = 1;
                        break;
                    }
                    else
                    {
                        flagnearTile = 0;
                    }
                }








            }




        }
        void CheckTileR2()
        {
            if (LHero.Count > 0 && LTileR2.Count > 0)
            {


                for (int i = 0; i < LTileR2.Count; i++)
                {
                    if (LHero[0].X > LTileR2[0].rcDst.X - 250 &&
                        LHero[0].X + LHero[0].imgs[0].Width < LTileR2[LTileR2.Count - 1].rcDst.X + LTileR2[LTileR2.Count - 1].rcDst.Width &&
                        LHero[0].Y + LHero[0].imgs[0].Height - 20 > LTileR2[i].rcDst.Y)
                    {
                        flagnearTile = 1;
                        break;
                    }
                    else
                    {
                        flagnearTile = 0;
                    }
                }








            }




        }
        void Gravity()
        {
            if (LHero.Count > 0)
            {
                if (LHero[0].Y < 500 && flagnearTile == 0 && flagnearladder == 0 && flongjump == 0 && fjump == 0)
                {
                    LHero[0].Y += 10;
                    if (LHeroRight.Count > 0)
                    {
                        LHeroRight[0].Y += 10;
                    }

                    if (LHeroLeft.Count > 0)
                    {
                        LHeroLeft[0].Y += 10;
                    }

                }
            }

        }
        void CreateElevator()
        {


            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            pnn.X = 250;
            pnn.Y = 450;
            Bitmap img = new Bitmap("DoorLocked.png");


            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.imgs.Add(img);


            img = new Bitmap("DoorOpen.png");

            pnn.imgs.Add(img);

            LElevator.Add(pnn);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            CreateHero();
            CreateMap1();
            CreateTile();
            CreateLadder();
            CreateMap2();
            createshieldenemy();
            CreateElevator();
            createhooktrap();
            createammosystem();
            createammo();
            createhealth();


            off = new Bitmap(ClientSize.Width, ClientSize.Height);


        }

        void jump()
        {
            if (fhookdir != 1)
            {

                if (jumpphase == 0)
                {
                    flagshow = 1;
                    if (jumponce == 0)
                    {
                        CreateHeroJump();
                        jumponce = 1;
                    }
                    LHeroJump[0].iFrame = (LHeroJump[0].iFrame + 1) % 3;
                    ct++;
                    if (LHero[0].Y > 420)
                    {
                        LHeroJump[0].iFrame++;

                        LHeroJump[0].Y -= 10;
                        LHero[0].Y -= 10;
                    }
                    else
                    {
                        jumpphase = 1;
                        flagshow = 0;
                        LHeroJump.RemoveAt(0);

                    }
                }

                if (jumpphase == 1)
                {

                    if (LHero[0].Y < 500)
                    {


                        LHero[0].Y += 10;

                    }
                    else
                    {
                        fjump = 0;
                        jumpphase = 0;
                        jumponce = 0;



                    }
                }

            }
        }
        void createshieldenemy()
        {

            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();
            for (int i = 0; i < 10; i++)
            {
                Bitmap img = new Bitmap("sh" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);
            }
            for (int i = 0; i < 9; i++)
            {
                Bitmap img = new Bitmap("shr" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);
            }
            pnn.X = LTile[0].rcDst.X + 280;
            pnn.Y = LTile[0].rcDst.Y - 180;
            lshieldenemy.Add(pnn);


        }

        void CheckSpikeStrap()
        {

            for (int i = 0; i < LSpikeR2.Count; i++)
            {
                for (int j = 0; j < LHero.Count; j++)
                {

                    if (LHero[j].X > LSpikeR2[i].rcDst.X && LHero[j].X < LSpikeR2[i].rcDst.X + LSpikeR2[i].rcDst.Width && LHero[0].Y < 80)
                    {

                        LHero[0].X += 20;

                        LHero[0].Y -= 10;
                    }
                }

            }



        }
        void CreateSpikeR2()
        {

            CAdvImag pnn;
            for (int i = 0; i < 4; i++)
            {
                pnn = new CAdvImag();
                pnn.img = new Bitmap("Spike.png");
                pnn.rcDst = new Rectangle(0 + cxmarginSpikeR2, 230, 100, 30);
                pnn.rcSrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
                if (i == 2)
                {
                    cxmarginSpikeR2 += 200;
                }
                LSpikeR2.Add(pnn);
                cxmarginSpikeR2 += 100;

            }


        }
        void CreateExplosion()
        {


            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();

            for (int i = 0; i < 6; i++)
            {
                Bitmap img = new Bitmap("E" + (i + 1) + ".gif");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.imgs.Add(img);

            }
            pnn.X = LBombEnemy[0].X - 100;

            pnn.Y = LBombEnemy[0].Y - 400;

            LBombEXP.Add(pnn);


        }
        void CreateBombDetect()
        {
            for (int j = 0; j < LHero.Count; j++)
            {
                for (int i = 0; i < LBombEnemy.Count; i++)
                {
                    //hero on the left 
                    if (LHero[j].X < LBombEnemy[i].X + LBombEnemy[i].imgs[0].Width - 600)
                    {

                        LBombEnemy[i].X -= 5;
                        if (LBombEnemy[i].iFrame < 9)
                        {
                            LBombEnemy[i].iFrame++;

                        }
                        else if (LBombEnemy[i].iFrame >= 0)
                        {
                            LBombEnemy[i].iFrame--;
                        }

                        if (LHero[j].X > LBombEnemy[i].X && LHero[j].X < LBombEnemy[i].X + LBombEnemy[i].imgs[0].Width - 600)
                        {
                            if (flagCreateExp == 0)
                            {
                                CreateExplosion();
                                flagCreateExp = 1;

                            }


                        }

                    }
                    else
                    {
                        LBombEnemy[i].X += 5;

                        if (oncef == 0)
                        {
                            LBombEnemy[i].iFrame = 11;

                            oncef = 1;

                        }

                        if (LBombEnemy[i].iFrame < 22 && fswitchbomb == 0)
                        {
                            LBombEnemy[i].iFrame++;
                        }
                        else
                        {
                            fswitchbomb = 1;
                        }

                        if (LBombEnemy[i].iFrame > 12 && fswitchbomb == 1)
                        {
                            LBombEnemy[i].iFrame--;
                        }
                        else
                        {
                            fswitchbomb = 0;
                        }
                        //hero on the left
                        if (LHero[j].X > LBombEnemy[i].X && LBombEnemy[i].X + LBombEnemy[i].imgs[0].Width - 600 > LHero[j].X)
                        {
                            if (flagCreateExp == 0)
                            {
                                CreateExplosion();
                                flagCreateExp = 1;

                            }


                        }
                    }




                }
            }


        }
        void createammo()
        {
            Random rr = new Random();
            CImagActor pnn = new CImagActor();
            pnn.X = rr.Next(0, ClientSize.Width);
            pnn.Y = 600;
            Bitmap img = new Bitmap("ammo.png");
            pnn.img = img;
            img.MakeTransparent(img.GetPixel(0, 0));
            lammo.Add(pnn);
        }
        void createammosystem()
        {
            CActor pnn = new CActor();
            pnn.X = 7;
            pnn.Y = 65;
            pnn.W = 80;
            pnn.H = 30;
            lammocounter.Add(pnn);
        }
        void collectammo()
        {
            for (int i = 0; i < lammo.Count; i++)
            {
                if (LHero[0].X - 60 + LHero[0].imgs[0].Width >= lammo[i].X && LHero[0].X - 60 + LHero[0].imgs[0].Width <= lammo[i].X + lammo[i].img.Width)
                {
                    if (lammo[i].Y - 80 <= LHero[0].Y + LHero[0].imgs[0].Height && lammo[i].Y - 80 >= LHero[0].Y)
                    {
                        lammo.RemoveAt(i);
                        value += 30;

                    }
                }
            }

        }
        void CreateBombEnemy()
        {

            CMultiImageActor pnn = new CMultiImageActor();
            pnn.imgs = new List<Bitmap>();

            for (int i = 0; i < 11; i++)
            {
                Bitmap img = new Bitmap("BL" + (i + 1) + ".png");

                pnn.imgs.Add(img);
            }

            for (int i = 0; i < 12; i++)
            {
                Bitmap img = new Bitmap("BR" + (i + 1) + ".png");

                pnn.imgs.Add(img);
            }

            pnn.X = 1000;

            pnn.Y = 200;
            LBombEnemy.Add(pnn);

        }
        void createhealth()
        {
            CActor pnn = new CActor();
            pnn.X = 7;
            pnn.Y = 35;
            pnn.W = 80;
            pnn.H = 30;
            lhealthsystem.Add(pnn);
        }
        void animateall()
        {
            if (health == 0)
            {
                LHero.Clear();
            }

            for (int i = 0; i < Lbullet.Count; i++)
            {
                if (LBombEnemy.Count > 0)
                {
                    if (Lbullet[i].Y >= LBombEnemy[0].Y && Lbullet[i].Y <= LBombEnemy[0].Y + LBombEnemy[0].imgs[0].Height)
                    {
                        if (LBombEnemy[0].X + LBombEnemy[0].imgs[0].Width >= Lbullet[i].X && LBombEnemy[0].X <= Lbullet[i].X)
                        {
                            LBombEnemy.RemoveAt(0);
                        }
                    }

                }
                if (Lbullet[i].freverse == 0)
                {
                    if (LHero.Count > 0)
                    {
                        if (Lbullet[i].X < LHero[0].X)
                        {

                            Lbullet[i].X -= 20;
                        }
                        else
                        {
                            Lbullet[i].X += 20;
                        }
                    }
                }
                if (lshieldenemy.Count > 0)
                {
                    if (Lbullet[i].Y >= lshieldenemy[0].Y && Lbullet[i].Y <= lshieldenemy[0].Y + lshieldenemy[0].imgs[0].Height)
                    {
                        if (lshieldenemy[0].X + lshieldenemy[0].imgs[0].Width >= Lbullet[i].X && lshieldenemy[0].X <= Lbullet[i].X)
                        {
                            if (LHero.Count > 0)
                            {
                                if (Lbullet[i].X < LHero[0].X)
                                {
                                    Lbullet[i].freverse = 1;
                                }
                                if (Lbullet[i].X > LHero[0].X)
                                {
                                    Lbullet[i].freverse = 2;

                                }
                            }
                            if (cthealthenemy == 3)
                            {
                                lshieldenemy.Clear();
                                Lbullet.Clear();
                                break;
                            }
                            cthealthenemy++;
                        }
                    }
                    if (Lbullet[i].freverse == 1)
                    {
                        if (LHero.Count > 0)
                        {
                            if (Lbullet[i].X < LHero[0].X)
                            {
                                Lbullet[i].X += 20;
                            }
                            else
                            {
                                if (cthealthero == 3)
                                {

                                    LHero.Clear();

                                }
                                cthealthero++;
                                health--;
                                Lbullet.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (Lbullet[i].freverse == 2)
                    {
                        if (LHero.Count > 0)
                        {
                            if (Lbullet[i].X > LHero[0].X)
                            {
                                Lbullet[i].X -= 20;

                            }
                            else
                            {

                                if (cthealthero == 3)
                                {
                                    LHero.Clear();

                                }
                                cthealthero++;
                                health--;

                                Lbullet.RemoveAt(i);
                                break;

                            }
                        }
                    }
                }


            }
            if (LHero.Count > 0)
            {

                if (fjump == 1)
                {

                    jump();

                }
                if (flongjump == 1)
                {

                    longjump();

                }
                if (lshieldenemy.Count > 0)
                {
                    if (lshieldenemy[0].Y <= LHero[0].Y && LHero[0].Y <= lshieldenemy[0].Y + lshieldenemy[0].imgs[0].Height)
                    {
                        if (lshieldenemy[0].X > LHero[0].X)
                        {




                            lshieldenemy[0].iFrame = (lshieldenemy[0].iFrame + 1) % 9;


                        }

                        else
                        {
                            lshieldenemy[0].iFrame++;
                            if (lshieldenemy[0].iFrame == 17)
                            {
                                lshieldenemy[0].iFrame = 10;

                            }
                        }
                        if (lshieldenemy[0].X - 80 + lshieldenemy[0].imgs[0].Width >= LHero[0].X && lshieldenemy[0].X + 40 + lshieldenemy[0].imgs[0].Width <= LHero[0].X + LHero[0].imgs[0].Width)
                        {
                            LHero.RemoveAt(0);
                            health = 0;

                        }
                        if (LHero.Count > 0)
                        {
                            if (lshieldenemy[0].X <= LHero[0].X + LHero[0].imgs[0].Width && lshieldenemy[0].X + 40 >= LHero[0].X)
                            {
                                health = 0;
                                LHero.RemoveAt(0);

                            }

                        }



                        if (LHero.Count > 0)
                        {
                            if (lshieldenemy[0].X > LHero[0].X)
                            {


                                lshieldenemy[0].X -= 2;





                            }


                            else
                            {
                                lshieldenemy[0].X += 2;



                            }
                        }
                    }

                    if (LHero.Count > 0)
                    {
                        if (lshieldenemy[0].X > LHero[0].X)
                        {
                            if (ctonce == 0)
                            {
                                ctonce++;
                                lshieldenemy[0].iFrame = 0;
                            }
                            ct2once = 0;
                        }


                        else
                        {
                            if (ct2once == 0)
                            {
                                ct2once++;
                                lshieldenemy[0].iFrame = 10;
                            }
                            ctonce = 0;
                        }


                    }
                }
                if (LHero.Count > 0 && lhooktrap.Count > 0)
                {
                    if (LHero[0].Y <= lhooktrap[0].Y + 50 && LHero[0].Y <= lhooktrap[0].Y - 50 && LHero[0].X >= lhooktrap[0].X)
                    {
                        fhooktrap = 1;
                        if (fhooktrap == 1)
                        {
                            if (fhookdir == 0)
                            {
                                hookwidth += 10;
                            }
                            if (hookwidth + lhooktrap[0].X + 10 > LHero[0].X && hookwidth + lhooktrap[0].X + 10 <= LHero[0].X + LHero[0].imgs[0].Width)
                            {
                                fhookdir = 1;
                            }

                            if (fhookdir == 1)
                            {
                                if (hookwidth > 0)
                                {
                                    hookwidth -= 10;
                                    LHero[0].X -= 10;

                                }
                                else
                                {
                                    fhookdir = 0;
                                    fhooktrap = 0;
                                    health--;
                                    lhooktrap.Clear();
                                }
                            }


                        }
                    }
                    else
                    {
                        hookwidth = 0;
                    }

                }

            }
            LMap1[0].rcSrc.X = XScroll;
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);

            for (int i = 0; i < LMap1.Count; i++)
            {
                CAdvImag ptrav = LMap1[i];

                g.DrawImage(ptrav.img, ptrav.rcDst, ptrav.rcSrc, GraphicsUnit.Pixel);
            }


            for (int i = 0; i < LMap2.Count; i++)
            {
                CAdvImag ptrav = LMap2[i];

                g.DrawImage(ptrav.img, ptrav.rcDst, ptrav.rcSrc, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < LTile.Count; i++)
            {
                CAdvImag ptrav = LTile[i];

                g.DrawImage(ptrav.img, ptrav.rcDst, ptrav.rcSrc, GraphicsUnit.Pixel);

            }
            for (int i = 0; i < LLadder.Count; i++)
            {
                CAdvImag ptrav = LLadder[i];

                g.DrawImage(ptrav.img, ptrav.rcDst, ptrav.rcSrc, GraphicsUnit.Pixel);

            }
            for (int i = 0; i < LElevator.Count; i++)
            {
                int k = LElevator[i].iFrame;
                g.DrawImage(LElevator[i].imgs[k], LElevator[i].X, LElevator[i].Y, LElevator[i].imgs[k].Width - 135, LElevator[i].imgs[k].Height - 200);
            }
            if (flagshow != 1)
            {
                for (int i = 0; i < LHero.Count; i++)
                {
                    int k = LHero[i].iFrame;
                    if (k > 1)
                    {
                        g.DrawImage(LHero[i].imgs[k], LHero[i].X, LHero[i].Y - 70, LHero[i].imgs[k].Width + 40, LHero[i].imgs[k].Height + 40);

                    }
                    else
                    {
                        g.DrawImage(LHero[i].imgs[k], LHero[i].X, LHero[i].Y, LHero[i].imgs[k].Width - 20, LHero[i].imgs[k].Height - 20);
                    }

                }

            }
            for (int i = 0; i < lshieldenemy.Count; i++)
            {
                int k = lshieldenemy[i].iFrame;
                if (k >= 10 && k <= 17)
                {
                    g.DrawImage(lshieldenemy[i].imgs[k], lshieldenemy[i].X, lshieldenemy[i].Y, lshieldenemy[i].imgs[k].Width - 40, lshieldenemy[i].imgs[k].Height);
                }
                else
                {
                    g.DrawImage(lshieldenemy[i].imgs[k], lshieldenemy[i].X, lshieldenemy[i].Y, lshieldenemy[i].imgs[k].Width, lshieldenemy[i].imgs[k].Height);
                }
            }



            if (LHeroRight.Count > 0)
            {
                for (int i = 0; i < LHeroRight.Count; i++)
                {
                    int k = LHeroRight[i].iFrame;
                    g.DrawImage(LHeroRight[i].imgs[k], LHeroRight[i].X, LHeroRight[i].Y, LHeroRight[i].imgs[k].Width + 50, LHeroRight[i].imgs[k].Height + 50);
                }

            }

            if (LHeroLeft.Count > 0)
            {
                for (int i = 0; i < LHeroLeft.Count; i++)
                {
                    int k = LHeroLeft[i].iFrame;
                    g.DrawImage(LHeroLeft[i].imgs[k], LHeroLeft[i].X, LHeroLeft[i].Y, LHeroLeft[i].imgs[k].Width + 50, LHeroLeft[i].imgs[k].Height + 50);
                }

            }
            if (LHeroJump.Count > 0)
            {
                for (int i = 0; i < LHeroJump.Count; i++)
                {
                    int k = LHeroJump[i].iFrame;
                    g.DrawImage(LHeroJump[i].imgs[k], LHeroJump[i].X, LHeroJump[i].Y, LHeroJump[i].imgs[k].Width + 50, LHeroJump[i].imgs[k].Height + 50);
                }
            }
            for (int i = 0; i < Lbullet.Count; i++)
            {
                g.DrawImage(Lbullet[i].img, Lbullet[i].X, Lbullet[i].Y, Lbullet[i].img.Width - 35, Lbullet[i].img.Height - 35);
            }
            for (int i = 0; i < lhooktrap.Count; i++)
            {
                g.DrawImage(lhooktrap[i].img, lhooktrap[i].X, lhooktrap[i].Y, lhooktrap[i].img.Width, lhooktrap[i].img.Height);

            }
            if (fhooktrap == 1)
            {
                Pen p = new Pen(Color.Yellow, 3);
                g.DrawLine(p, lhooktrap[0].X + lhooktrap[0].img.Width - 20, lhooktrap[0].Y + lhooktrap[0].img.Height / 2 - 15, lhooktrap[0].X + lhooktrap[0].img.Width + hookwidth, lhooktrap[0].Y + lhooktrap[0].img.Height / 2 - 15);
            }
            for (int i = 0; i < lhooktrap.Count; i++)
            {
                g.DrawImage(lhooktrap[i].img, lhooktrap[i].X, lhooktrap[i].Y, lhooktrap[i].img.Width, lhooktrap[i].img.Height);
            }
            for (int i = 0; i < LTileR2.Count; i++)
            {
                CAdvImag ptrav = LTileR2[i];

                g.DrawImage(ptrav.img, ptrav.rcDst, ptrav.rcSrc, GraphicsUnit.Pixel);

            }
            //Y
            for (int i = 0; i < LSpikeR2.Count; i++)
            {
                CAdvImag ptrav = LSpikeR2[i];

                g.DrawImage(ptrav.img, ptrav.rcDst, ptrav.rcSrc, GraphicsUnit.Pixel);

            }
            for (int i = 0; i < LBombEnemy.Count; i++)
            {
                int k = LBombEnemy[i].iFrame;

                g.DrawImage(LBombEnemy[i].imgs[k], LBombEnemy[i].X, LBombEnemy[i].Y + 300, LBombEnemy[i].imgs[k].Width - 600, LBombEnemy[i].imgs[k].Height - 400);


            }
            for (int i = 0; i < LlaserEnemy.Count; i++)
            {
                int k = LlaserEnemy[i].iFrame;
                if (k >= 0 && k < LlaserEnemy[i].imgs.Count)
                {
                    g.DrawImage(LlaserEnemy[i].imgs[k], LlaserEnemy[i].X, LlaserEnemy[i].Y + 420, LlaserEnemy[i].imgs[k].Width, LlaserEnemy[i].imgs[k].Height + 5);
                }
            }
            //Y

            for (int i = 0; i < Llasers.Count; i++)
            {
                int k = Llasers[i].iFrame;
                if (k >= 0 && k < Llasers[i].imgs.Count)
                {
                    g.DrawImage(Llasers[i].imgs[k], Llasers[i].X, Llasers[i].Y + 420, Llasers[i].imgs[k].Width - 50 + increaselaserW, Llasers[i].imgs[k].Height - 85);
                }
            }
            //Y




            for (int i = 0; i < LBombEXP.Count; i++)
            {
                int k = LBombEXP[i].iFrame;

                g.DrawImage(LBombEXP[i].imgs[k], LBombEXP[i].X, LBombEXP[i].Y + 500, LBombEXP[i].imgs[k].Width + 300, LBombEXP[i].imgs[k].Height + 300);


            } //Y


            for (int i = 0; i < lammo.Count; i++)
            {
                g.DrawImage(lammo[i].img, lammo[i].X, lammo[i].Y, lammo[i].img.Width - 80, lammo[i].img.Height - 80);
            }
            for (int i = 0; i < lammocounter.Count; i++)
            {
                SolidBrush brsh = new SolidBrush(Color.White);
                g.FillRectangle(brsh, lammocounter[0].X, lammocounter[0].Y + 30, lammocounter[0].W, lammocounter[0].H);
            }
            int fontSize = 20;
            Font font = new Font("Arial", fontSize, FontStyle.Regular);
            g.DrawString("Ammo ", font, Brushes.White, lammocounter[0].X, lammocounter[0].Y);

            g.DrawString(value.ToString(), font, Brushes.Black, lammocounter[0].X + 20, lammocounter[0].Y + 30);

            for (int i = 0; i < lhealthsystem.Count; i++)
            {
                SolidBrush brsh = new SolidBrush(Color.White);
                g.FillRectangle(brsh, lhealthsystem[0].X + 40, lhealthsystem[0].Y, lhealthsystem[0].W, lhealthsystem[0].H);
            }
            g.DrawString(health.ToString(), font, Brushes.Black, lhealthsystem[0].X + 55, lhealthsystem[0].Y);

            Bitmap image = new Bitmap("health.png");
            image.MakeTransparent(image.GetPixel(0, 0));
            g.DrawImage(image, lhealthsystem[0].X - 20, lhealthsystem[0].Y - 10, image.Width - 50, image.Height - 50);


        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);

        }


    }
}