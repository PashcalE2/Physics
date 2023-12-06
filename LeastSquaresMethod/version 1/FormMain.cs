using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace LeastSquaresMethod
{
    public partial class FormMain : Form
    {
        public struct PointD
        {
            public double X;
            public double Y;
        }

        public static bool isMoveCenter = false;

        public static Color BackGroundColor = Color.FromArgb(255, 255, 255, 255);
        public static Color AxisXCrossColor = Color.FromArgb(255, 255, 0, 0);
        public static Color AxisYCrossColor = Color.FromArgb(255, 0, 0, 255);

        public static double ArrowAngleRad = 15 * Math.PI / 180;
        public static int ArrowLength = 10;

        public static int LastBlockResize = 0;
        public static int BlockResizeStage = 0;
        public static int BlockDefaultSizePixels = 50;
        public static int BlockSizePixels = BlockDefaultSizePixels;
        public static float BlockCoefficientSizePixels = (float)1.1;
        public static PointF BlockScale = new PointF((float)1.0, (float)1.0); // Kx Ky
        public static PointF LastBlockScale = BlockScale; // Kx Ky
        public static PointF PointScale = new PointF((float)5.0, (float)5.0);

        public static int SmallDashToBig = 5;

        public static Color PenSolidColor = Color.FromArgb(255, 0, 0, 0);
        public static Color PenBigDashColor = Color.FromArgb(255, 200, 200, 200);
        public static Color PenSmallDashColor = Color.FromArgb(255, 230, 230, 230);

        public static int PenWidth = 1;

        public static Font TextFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);

        public static Point PicScale = new Point();
        public static Point PicHalfScale = new Point();
        public static PointF CoordCenter = new PointF(0, 0);
        public static Point CoordOffset = new Point(0, 0);
        public static List<PointF> Points = new List<PointF>();

        public static Point MousePos = new Point(0, 0);

        public static Hashtable LSMAns = new Hashtable();

        public static string LastTextBoxAxisXScaleText = "1";
        public static string LastTextBoxAxisYScaleText = "1";

        public FormMain()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(MouseWheelEvent); // добавление ивента MouseWheelEvent на колесико мыши
        }

        public List<string> Explode(string Str, string Exp) // разрывание строки по подстрокам, например Explode("1 2", " ") даст "1" и "2"
        {
            List<string> Ans = new List<string>();

            if ((Str.Length == 0) || (Exp.Length == 0))
                return Ans;

            int left = 0;
            int i = 0;
            while (i < Str.Length - Exp.Length) 
            {
                bool flag = true;
                for (int j = 0; j < Exp.Length; j++)
                {
                    if (Str[i + j] != Exp[j])
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    Ans.Add(Str.Substring(left, i - left));
                    i += Exp.Length - 1;
                    left = i + 1;
                }

                i++;
            }

            Ans.Add(Str.Substring(left, i - left + 1));

            return Ans;
        }

        public bool IsCorrectFloat(string Str)
        {
            Str = Str.Replace(".", ",");
            if ((Str.IndexOf("-") > 0) || (Str.IndexOf(",", Str.IndexOf(",") + 1) > 0) || (Str[0] == ',') || (Str[Str.Length - 1] == ','))
                return false;

            for (int i = 0; i < Str.Length; i++)
            {
                if (((byte)Str[i] != 13) && (Str[i] != '-') && (Str[i] != ',') && ((Str[i] < '0') || (Str[i] > '9')))
                {
                    return false;
                }
            }
            
            return true;
        }

        public bool IsLSMAnsCorrect(Hashtable LSM)
        {
            return LSM.ContainsKey("B") && LSM.ContainsKey("A") && LSM.ContainsKey("P(0; Y)") && LSM.ContainsKey("P(X; 0)") && LSM.ContainsKey("D") && LSM.ContainsKey("Sb") && LSM.ContainsKey("Sa") && LSM.ContainsKey("Delta_B") && LSM.ContainsKey("Delta_A") && (((double)LSM["B"] != 0) || ((double)LSM["A"] != 0));
        }

        public string FormatSave()
        {
            return string.Format("Наклонный коэффициент прямой: {0}\nСмещение прямой по оси Oy: {1}\nПересечение с осью Oy: {2}\nПересечение с осью Ox: {3}\nПараметр D: {4}\nСКО наклонного коэффициента: {5}\nСКО смещения: {6}\nПогрешность коэффициента: {7}\nПогрешность смещения: {8}", (double)LSMAns["B"], (double)LSMAns["A"], (double)LSMAns["P(0; Y)"], (double)LSMAns["P(X; 0)"], (double)LSMAns["D"], (double)LSMAns["Sb"], (double)LSMAns["Sa"], (double)LSMAns["Delta_B"], (double)LSMAns["Delta_A"]);
        }

        private bool Inrange(int Value, int Left, int Right)
        {
            return (Value >= Left) & (Value <= Right);
        }

        private bool Inrange(float Value, float Left, float Right)
        {
            return (Value >= Left) & (Value <= Right);
        }

        private int Clamp(int Value, int Left, int Right)
        {
            return (Value > Right) ? Right : ((Value < Left) ? Left : Value);
        }

        private float Clamp(float Value, float Left, float Right)
        {
            return (Value > Right) ? Right : ((Value < Left) ? Left : Value);
        }

        public bool IsTextRectCorrect(RectangleF Rect)
        {
            return Inrange(Rect.X, -1, Pic.Width) && Inrange(Rect.X + Rect.Width, -1, Pic.Width) && Inrange(Rect.Y, -1, Pic.Height) && Inrange(Rect.Y + Rect.Height, -1, Pic.Height);
        }

        public bool IsPointInRect(PointF Point, RectangleF Rect)
        {
            return Inrange(Point.X, Rect.X, Rect.X + Rect.Width) && Inrange(Point.Y, Rect.Y, Rect.Y + Rect.Height);
        }

        public bool IsTextCollides(RectangleF Rect, List<RectangleF> Rects)
        {
            for (int i = 0; i < Rects.Count; i++)
            {
                if (IsPointInRect(new PointF(Rect.X, Rect.Y), Rects[i]) || IsPointInRect(new PointF(Rect.X + Rect.Width, Rect.Y), Rects[i]) || IsPointInRect(new PointF(Rect.X, Rect.Y + Rect.Height), Rects[i]) || IsPointInRect(new PointF(Rect.X + Rect.Width, Rect.Y + Rect.Height), Rects[i]) 
                    || 
                    IsPointInRect(new PointF(Rects[i].X, Rects[i].Y), Rect) || IsPointInRect(new PointF(Rects[i].X + Rects[i].Width, Rects[i].Y), Rect) || IsPointInRect(new PointF(Rects[i].X, Rects[i].Y + Rects[i].Height), Rect) || IsPointInRect(new PointF(Rects[i].X + Rects[i].Width, Rects[i].Y + Rects[i].Height), Rect))
                {
                    return true;
                }
            }

            return false;
        }

        private Hashtable LeastSquaresMethod(List<PointF> Points)
        {
            Hashtable Ans = new Hashtable();
            Ans.Add("B", 0D);
            Ans.Add("A", 0D);
            Ans.Add("P(0; Y)", 0D);
            Ans.Add("P(X; 0)", 0D);
            Ans.Add("D", 0D);
            Ans.Add("Sb", 0D);
            Ans.Add("Sa", 0D);
            Ans.Add("Delta_B", 0D);
            Ans.Add("Delta_A", 0D);

            if (Points.Count < 2)
                return Ans;

            PointD Average = new PointD
            {
                X = 0,
                Y = 0
            };

            for (int i = 0; i < Points.Count; i++)
            {
                Average.X += Points[i].X;
                Average.Y += Points[i].Y;
            }

            Average.X /= Points.Count;
            Average.Y /= Points.Count;

            double B_Numenator = 0;
            double B_Denominator = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                B_Numenator += (Points[i].X - Average.X) * (Points[i].Y - Average.Y);
                B_Denominator += (Points[i].X - Average.X) * (Points[i].X - Average.X);
            }

            double B = B_Numenator / B_Denominator;
            double A = Average.Y - B * Average.X;

            // угол наклона и смещение прямой y = Bx + A

            Ans["B"] = B;
            Ans["A"] = A;

            Ans["P(0; Y)"] = A;
            Ans["P(X; 0)"] = -A / B;

            double d = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                d += Math.Pow(Points[i].Y - (A + B * Points[i].X), 2);
            }

            double D = B_Denominator;
            Ans["D"] = D;

            double Sb = Math.Sqrt(d / (D * (Points.Count - 2)));
            Ans["Sb"] = Sb;

            double Sa = Math.Sqrt((D + Points.Count * Average.X * Average.X) * d / (Points.Count * D * (Points.Count - 2)));
            Ans["Sa"] = Sa;

            // погрешности

            Ans["Delta_B"] = 2 * Sb;
            Ans["Delta_A"] = 2 * Sa;

            return Ans;
        }

        private void Arrow(Graphics G, Point From, Point To, int Length, double AngleRad)
        {
            Pen Pensil = new Pen(PenSolidColor, PenWidth);

            G.DrawLine(Pensil, From, To);

            Point Dir = new Point(To.X - From.X, To.Y - From.Y);
            double DirLen = Math.Sqrt(Dir.X * Dir.X + Dir.Y * Dir.Y);

            Point ArrowDir = new Point();

            ArrowDir.X = (int) ((Dir.X * Math.Cos(AngleRad) - Dir.Y * Math.Sin(AngleRad)) * Length / DirLen);
            ArrowDir.Y = (int) ((Dir.X * Math.Sin(AngleRad) + Dir.Y * Math.Cos(AngleRad)) * Length / DirLen);

            G.DrawLine(Pensil, To, new Point(To.X - ArrowDir.X, To.Y - ArrowDir.Y));

            ArrowDir.X = (int) ((Dir.X * Math.Cos(AngleRad) + Dir.Y * Math.Sin(AngleRad)) * Length / DirLen);
            ArrowDir.Y = (int) ((-Dir.X * Math.Sin(AngleRad) + Dir.Y * Math.Cos(AngleRad)) * Length / DirLen);

            G.DrawLine(Pensil, To, new Point(To.X - ArrowDir.X, To.Y - ArrowDir.Y));

            Pensil.Dispose();
        }

        public void DrawEqLine(Graphics G, float K, float B, float LeftX, float RightX, Point Pos, int Width)
        {
            Pen Pensil = new Pen(Color.FromArgb(150, 0, 0, 0), Width);

            G.DrawLine(Pensil, new PointF(-Pos.X + PicHalfScale.X + LeftX * BlockSizePixels / BlockScale.X, Pos.Y + PicHalfScale.Y - (K * LeftX + B) * BlockSizePixels / BlockScale.Y), new PointF(-Pos.X + PicHalfScale.X + RightX * BlockSizePixels / BlockScale.X, Pos.Y + PicHalfScale.Y - (K * RightX + B) * BlockSizePixels / BlockScale.Y));

            Pensil.Dispose();
        }

        private void DrawFlat(Graphics G, Point Pos)
        {
            Point PosClamp = new Point(Clamp(-Pos.X + PicHalfScale.X, 0, PicScale.X), Clamp(Pos.Y + PicHalfScale.Y, 0, PicScale.Y));

            Pen PensilSmallDash = new Pen(PenSmallDashColor, 1);
            Pen PensilBigDash = new Pen(PenBigDashColor, 1);
           
            PointF TextAxisYMaxScale = new PointF(0, 0);
            PointF AxisXRange = new PointF(0, 0);

            // мелкая сетка

            // горизонтальные

            float horiz;
            float vertical;

            if (checkBoxSmallDash.Checked)
            {
                horiz = Pos.Y + PicHalfScale.Y;
                while (horiz > 0)
                {
                    if (horiz < PicScale.Y)
                        G.DrawLine(PensilSmallDash, 0, horiz, PicScale.X, horiz);
                    horiz -= (float)BlockSizePixels / SmallDashToBig;
                }

                horiz = Pos.Y + PicHalfScale.Y;
                while (horiz < PicScale.Y)
                {
                    if (horiz > 0)
                        G.DrawLine(PensilSmallDash, 0, horiz, PicScale.X, horiz);
                    horiz += (float)BlockSizePixels / SmallDashToBig;
                }

                // вертикальные 

                vertical = -Pos.X + PicHalfScale.X;
                while (vertical > 0)
                {
                    if (vertical < PicScale.X)
                        G.DrawLine(PensilSmallDash, vertical, 0, vertical, PicScale.Y);
                    vertical -= (float)BlockSizePixels / SmallDashToBig;
                }

                vertical = -Pos.X + PicHalfScale.X;
                while (vertical < PicScale.X)
                {
                    if (vertical > 0)
                        G.DrawLine(PensilSmallDash, vertical, 0, vertical, PicScale.Y);
                    vertical += (float)BlockSizePixels / SmallDashToBig;
                }
            }

            // большая сетка

            // горизонтальные

            int offset = 0;
            horiz = Pos.Y + PicHalfScale.Y;
            while (horiz > 0) 
            {
                if (horiz < PicScale.Y)
                {
                    string Text = (offset * BlockScale.Y).ToString();
                    SizeF TextSize = G.MeasureString(Text, TextFont);

                    if (TextAxisYMaxScale.X < TextSize.Width)
                    {
                        TextAxisYMaxScale.X = TextSize.Width;
                        TextAxisYMaxScale.Y = TextSize.Height;
                    }

                    if (checkBoxBigDash.Checked)
                        G.DrawLine(PensilBigDash, 0, horiz, PicScale.X, horiz);
                }

                offset++;

                horiz -= BlockSizePixels;
            }

            offset = 0;
            horiz = Pos.Y + PicHalfScale.Y;
            while (horiz < PicScale.Y) 
            {
                if (horiz > 0)
                {
                    string Text = (offset * BlockScale.Y).ToString();
                    SizeF TextSize = G.MeasureString(Text, TextFont);

                    if (TextAxisYMaxScale.X < TextSize.Width)
                    {
                        TextAxisYMaxScale.X = TextSize.Width;
                        TextAxisYMaxScale.Y = TextSize.Height;
                    }

                    if (checkBoxBigDash.Checked)
                        G.DrawLine(PensilBigDash, 0, horiz, PicScale.X, horiz);
                }

                offset--;

                horiz += BlockSizePixels;
            }

            // вертикальные

            offset = 0;
            vertical = -Pos.X + PicHalfScale.X;
            while (vertical > 0)
            {
                if (checkBoxBigDash.Checked && (vertical < PicScale.X))
                    G.DrawLine(PensilBigDash, vertical, 0, vertical, PicScale.Y);

                offset--;

                vertical -= BlockSizePixels;
            }

            AxisXRange.X = offset * BlockScale.X; // left

            offset = 0;
            vertical = -Pos.X + PicHalfScale.X;
            while (vertical < PicScale.X)
            {
                if (checkBoxBigDash.Checked && (vertical > 0))
                    G.DrawLine(PensilBigDash, vertical, 0, vertical, PicScale.Y);

                offset++;

                vertical += BlockSizePixels;
            }

            AxisXRange.Y = offset * BlockScale.X; // right

            // оси

            if (Inrange(-Pos.X + PicHalfScale.X, 0, PicScale.X))
            {
                // ось X
                Arrow(G, new Point(PosClamp.X, PicScale.Y), new Point(PosClamp.X, 0), ArrowLength, ArrowAngleRad);
            }

            if (Inrange(Pos.Y + PicHalfScale.Y, 0, PicScale.Y))
            {
                // ось Y
                Arrow(G, new Point(0, PosClamp.Y), new Point(PicScale.X, PosClamp.Y), ArrowLength, ArrowAngleRad);
            }

            // значения

            List<RectangleF> TextsRects = new List<RectangleF>();
            List<string> Texts = new List<string>();

            Brush RectBrush = new SolidBrush(BackGroundColor);
            Brush TextBrush = new SolidBrush(PenSolidColor);
            
            PointF TextAxisOffsetPixels = new PointF(Clamp(PosClamp.Y, 0, PicScale.Y - (TextAxisYMaxScale.Y + 7)), Clamp(PosClamp.X, TextAxisYMaxScale.X, PicScale.X)); // Axis X Offset, Axis Y Offset

            // на оси Y

            offset = 0;
            vertical = Pos.Y + PicHalfScale.Y;
            while (vertical > 0)
            {
                string Text = (offset * BlockScale.Y).ToString();
                SizeF TextSize = G.MeasureString(Text, TextFont);

                offset++;

                if (Text != "0")
                {
                    PointF TextPos = new PointF(TextAxisOffsetPixels.Y - TextSize.Width - 1, vertical - TextAxisYMaxScale.Y / 2);
                    RectangleF Rect = new RectangleF(TextPos, TextSize);

                    if (IsTextRectCorrect(Rect))
                    {
                        TextsRects.Add(Rect);
                        Texts.Add(Text);
                    }
                }

                vertical -= BlockSizePixels;
            }

            offset = 0;
            vertical = Pos.Y + PicHalfScale.Y;
            while (vertical < PicScale.Y)
            {
                string Text = (offset * BlockScale.Y).ToString();
                SizeF TextSize = G.MeasureString(Text, TextFont);

                offset--;

                if (Text != "0")
                {
                    PointF TextPos = new PointF(TextAxisOffsetPixels.Y - TextSize.Width - 1, vertical - TextAxisYMaxScale.Y / 2);
                    RectangleF Rect = new RectangleF(TextPos, TextSize);

                    if (IsTextRectCorrect(Rect))
                    {
                        TextsRects.Add(Rect);
                        Texts.Add(Text);
                    }
                }

                vertical += BlockSizePixels;
            }

            // на оси X

            offset = 0;
            horiz = -Pos.X + PicHalfScale.X;
            while (horiz < PicScale.X)
            {
                string Text = (offset * BlockScale.X).ToString();
                SizeF TextSize = G.MeasureString(Text, TextFont);

                offset++;

                if (Text != "0")
                {
                    PointF TextPos = new PointF(horiz - TextSize.Width / 2, TextAxisOffsetPixels.X + TextSize.Height / 4);
                    RectangleF Rect = new RectangleF(TextPos, TextSize);

                    if (IsTextRectCorrect(Rect) && !IsTextCollides(Rect, TextsRects))
                    {
                        TextsRects.Add(Rect);
                        Texts.Add(Text);
                    }
                }

                horiz += BlockSizePixels;
            }

            offset = 0;
            horiz = -Pos.X + PicHalfScale.X;
            while (horiz > 0)
            {
                string Text = (offset * BlockScale.X).ToString();
                SizeF TextSize = G.MeasureString(Text, TextFont);

                offset--;

                if (Text != "0")
                {
                    PointF TextPos = new PointF(horiz - TextSize.Width / 2, TextAxisOffsetPixels.X + TextSize.Height / 4);
                    RectangleF Rect = new RectangleF(TextPos, TextSize);

                    if (IsTextRectCorrect(Rect) && !IsTextCollides(Rect, TextsRects))
                    {
                        TextsRects.Add(Rect);
                        Texts.Add(Text);
                    }
                }

                horiz -= BlockSizePixels;
            }

            // рисование всех текстов (чисел)

            for (int i = 0; i < TextsRects.Count; i++)
            {
                G.FillRectangle(RectBrush, TextsRects[i]);
                G.DrawString(Texts[i], TextFont, TextBrush, TextsRects[i]);
            }

            // точки
            Brush EllipseBrush = new SolidBrush(PenSolidColor);

            for (int i = 0; i < Points.Count; i++)
            {
                RectangleF PointRect = new RectangleF(-Pos.X + PicHalfScale.X + Points[i].X * BlockSizePixels / BlockScale.X - PointScale.X / 2, Pos.Y + PicHalfScale.Y - Points[i].Y * BlockSizePixels / BlockScale.Y - PointScale.Y / 2, PointScale.X, PointScale.Y);
                G.FillEllipse(EllipseBrush, PointRect);
            }

            EllipseBrush.Dispose();
            // прямая

            // LSM

            Hashtable LSM = LeastSquaresMethod(Points); // "B", "A", "D", "Sb", "Sa", "Delta_B", "Delta_A"

            if ((Points.Count > 1) && IsLSMAnsCorrect(LSM)) {
                LSMAns = LSM;

                if (checkBoxDrawLine.Checked)
                    DrawEqLine(G, (float)(double)LSM["B"], (float)(double)LSM["A"], AxisXRange.X, AxisXRange.Y, Pos, 3);

                if (checkBoxAxisXCross.Checked)
                {
                    EllipseBrush = new SolidBrush(AxisXCrossColor);

                    RectangleF PointRect = new RectangleF(-Pos.X + PicHalfScale.X + (float)(double)LSM["P(X; 0)"] * BlockSizePixels / BlockScale.X - PointScale.X / 2, Pos.Y + PicHalfScale.Y - PointScale.Y / 2, PointScale.X, PointScale.Y);
                    G.FillEllipse(EllipseBrush, PointRect);

                    EllipseBrush.Dispose();
                }

                if (checkBoxAxisYCross.Checked)
                {
                    EllipseBrush = new SolidBrush(AxisYCrossColor);

                    RectangleF PointRect = new RectangleF(-Pos.X + PicHalfScale.X - PointScale.X / 2, Pos.Y + PicHalfScale.Y - (float)(double)LSM["P(0; Y)"] * BlockSizePixels / BlockScale.Y - PointScale.Y / 2, PointScale.X, PointScale.Y);
                    G.FillEllipse(EllipseBrush, PointRect);

                    EllipseBrush.Dispose();
                }
            }

            PensilBigDash.Dispose();
            PensilSmallDash.Dispose();
            RectBrush.Dispose();
            TextBrush.Dispose();
        }

        private void RefreshPic()
        {
            Bitmap BufferBitmap = new Bitmap(PicScale.X, PicScale.Y);
            Graphics BufferGraphics = Graphics.FromImage(BufferBitmap);

            BufferGraphics.Clear(BackGroundColor);
            DrawFlat(BufferGraphics, new Point((int)(CoordCenter.X * BlockSizePixels / BlockScale.X), (int)(CoordCenter.Y * BlockSizePixels / BlockScale.Y)));

            Graphics PicGraphics = Graphics.FromImage(Pic.Image);
            PicGraphics.DrawImage(BufferBitmap, 0, 0);

            BufferGraphics.Dispose();
            BufferBitmap.Dispose();
            PicGraphics.Dispose();

            Pic.Invalidate();
        }

        private void GetObjInfo(object sender, MouseEventArgs e)
        {
            // может как нибудь потом
        }

        private void MoveCoordCenterStart(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoveCenter = true;
                CoordOffset.X = e.X;
                CoordOffset.Y = e.Y;
            }
        }

        private void MoveCoordCenterLeave(object sender, MouseEventArgs e)
        {
            isMoveCenter = false;
        }

        private void MoveCoordCenter(object sender, MouseEventArgs e)
        {
            if (isMoveCenter)
            {
                CoordCenter.X = CoordCenter.X - (e.X - CoordOffset.X) * BlockScale.X / BlockSizePixels;
                CoordCenter.Y = CoordCenter.Y + (e.Y - CoordOffset.Y) * BlockScale.Y / BlockSizePixels;

                CoordOffset.X = e.X;
                CoordOffset.Y = e.Y;

                RefreshPic();
            }
        }

        private void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (BlockResizeStage < 7)
                    BlockSizePixels = (int)Math.Ceiling((double)BlockSizePixels * BlockCoefficientSizePixels);

                bool once = true;
                int BlockResizeStageSign = BlockResizeStage < 0 ? 1 : 0;
                if ((BlockSizePixels > BlockDefaultSizePixels * 2.5) && (Math.Abs(BlockResizeStage + BlockResizeStageSign) % 3 == 1) || (BlockSizePixels > BlockDefaultSizePixels * 2) && (Math.Abs(BlockResizeStage + BlockResizeStageSign) % 3 != 1))
                {
                    if (BlockResizeStage < 0)
                    {
                        BlockResizeStage++;
                        once = false;
                    }

                    if (Math.Abs(BlockResizeStage) % 3 == 1)
                    {
                        BlockScale.X = (float)(BlockScale.X / 2.5);
                        BlockScale.Y = (float)(BlockScale.Y / 2.5);

                        BlockSizePixels = BlockDefaultSizePixels;
                    }
                    else
                    {
                        BlockScale.X /= 2;
                        BlockScale.Y /= 2;

                        BlockSizePixels = BlockDefaultSizePixels;
                    }

                    if (BlockResizeStage >= 0 && once)
                    {
                        BlockResizeStage++;
                    }
                }

                RefreshPic();
            } 
            else if (e.Delta < 0)
            {
                BlockSizePixels = (int)Math.Ceiling((double)BlockSizePixels / BlockCoefficientSizePixels);

                bool once = true;
                int BlockResizeStageSign = BlockResizeStage < 0 ? 1 : 0;
                if ((BlockSizePixels < BlockDefaultSizePixels * 0.8) && (Math.Abs(BlockResizeStage + BlockResizeStageSign) % 3 == 1) || (BlockSizePixels < BlockDefaultSizePixels) && (Math.Abs(BlockResizeStage + BlockResizeStageSign) % 3 != 1))
                {
                    if (BlockResizeStage > 0)
                    {
                        BlockResizeStage--;
                        once = false;
                    }

                    if (Math.Abs(BlockResizeStage) % 3 == 1)
                    {
                        BlockScale.X = (float)(BlockScale.X * 2.5);
                        BlockScale.Y = (float)(BlockScale.Y * 2.5);

                        BlockSizePixels = (int)(BlockDefaultSizePixels * 2.3);
                    }
                    else
                    {
                        BlockScale.X *= 2;
                        BlockScale.Y *= 2;

                        BlockSizePixels = (int)(BlockDefaultSizePixels * 1.6);
                    }

                    if (BlockResizeStage <= 0 && once)
                    {
                        BlockResizeStage--;
                    }
                }

                RefreshPic();
            }

            
        }

        private void FullLoad(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text files(*.txt)|*.txt";

            PicScale = new Point(Pic.Width, Pic.Height);

            PicHalfScale = new Point(PicScale.X / 2, PicScale.Y / 2);

            Pic.Image = new Bitmap(Pic.Width, Pic.Height);

            RefreshPic();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog.FileName;

            string fileText = System.IO.File.ReadAllText(filename).Trim(' ');

            List<string> Lines = Explode(fileText, "\n");

            Points.Clear();
            for (int i = 0; i < Lines.Count; i++)
            {
                List<string> PointStr = Explode(Lines[i], " ");
                if (PointStr.Count == 2 && IsCorrectFloat(PointStr[0]) && IsCorrectFloat(PointStr[1]))
                    Points.Add(new PointF(Convert.ToSingle(PointStr[0].Replace('.', ',')), Convert.ToSingle(PointStr[1].Replace('.', ','))));
            }

            if (Points.Count < 2)
            {
                MessageBox.Show("Ошибка! Точек должно быть не менее двух.");
                return;
            }

            RefreshPic();
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text files(*.txt)|*.txt";

            if (!IsLSMAnsCorrect(LSMAns))
            {
                MessageBox.Show("Ошибка! Точек должно быть не менее двух.");
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog.FileName;

            System.IO.File.WriteAllText(filename, FormatSave());
            MessageBox.Show("Файл сохранен");
        }

        private void buttonTakeScreenshot_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog.FileName;

            Pic.Image.Save(filename);
            MessageBox.Show("Скриншот сохранен");
        }

        private void KeyEnterAxisXRescale(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string Text = textBoxAxisXScale.Text.Replace(".", ",");

                if (IsCorrectFloat(Text))
                {
                    float TextF = Convert.ToSingle(Text);
                    BlockScale.X = TextF * BlockScale.X / LastBlockScale.X;
                    LastBlockScale.X = TextF;
                    LastTextBoxAxisXScaleText = Text;

                    RefreshPic();
                } 
                else
                {
                    textBoxAxisXScale.Text = LastTextBoxAxisXScaleText;
                }
            }
        }

        private void FocusAxisXRescale(object sender, EventArgs e)
        {
            string Text = textBoxAxisXScale.Text.Replace(".", ",");

            if (IsCorrectFloat(Text))
            {
                float TextF = Convert.ToSingle(Text);
                BlockScale.X = TextF * BlockScale.X / LastBlockScale.X;
                LastBlockScale.X = TextF;
                LastTextBoxAxisXScaleText = Text;

                RefreshPic();
            }
            else
            {
                textBoxAxisXScale.Text = LastTextBoxAxisXScaleText;
            }
        }

        private void KeyEnterAxisYRescale(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string Text = textBoxAxisYScale.Text.Replace(".", ",");

                if (IsCorrectFloat(Text))
                {
                    float TextF = Convert.ToSingle(Text);
                    BlockScale.Y = TextF * BlockScale.Y / LastBlockScale.Y;
                    LastBlockScale.Y = TextF;
                    LastTextBoxAxisYScaleText = Text;

                    RefreshPic();
                }
                else
                {
                    textBoxAxisYScale.Text = LastTextBoxAxisYScaleText;
                }
            }
        }

        private void FocusAxisYRescale(object sender, EventArgs e)
        {
            string Text = textBoxAxisYScale.Text.Replace(".", ",");

            if (IsCorrectFloat(Text))
            {
                float TextF = Convert.ToSingle(Text);
                BlockScale.Y = TextF * BlockScale.Y / LastBlockScale.Y;
                LastBlockScale.Y = TextF;
                LastTextBoxAxisYScaleText = Text;

                RefreshPic();
            }
            else
            {
                textBoxAxisYScale.Text = LastTextBoxAxisYScaleText;
            }
        }

        private void BigDashCheckedChanged(object sender, EventArgs e)
        {
            RefreshPic();
        }

        private void SmallDashCheckedChanged(object sender, EventArgs e)
        {
            RefreshPic();
        }

        private void DrawLineCheckedChanged(object sender, EventArgs e)
        {
            RefreshPic();
        }

        private void AxisXCrossCheckedChanged(object sender, EventArgs e)
        {
            RefreshPic();
        }

        private void AxisYCrossCheckedChanged(object sender, EventArgs e)
        {
            RefreshPic();
        }

        private void Resizing(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            if (WindowState == FormWindowState.Minimized)
                return;

            if (control.Size.Width < 636)
            {
                control.Size = new Size(636, control.Size.Height);
            }

            if (control.Size.Height < 544)
            {
                control.Size = new Size(control.Size.Width, 544);
            }

            // вся панель

            panel1.Size = new Size(control.Size.Width - 36, control.Size.Height - 59);

            // кнопки справа картинки

            panel2.Location = new Point(panel1.Size.Width - 180, panel2.Location.Y);

            // изменение пикчи

            Pic.Size = new Size(panel1.Size.Width - 200, panel1.Size.Height - 85);

            PicScale = new Point(Pic.Width, Pic.Height);

            PicHalfScale = new Point(PicScale.X / 2, PicScale.Y / 2);

            Pic.Image = new Bitmap(PicScale.X, PicScale.Y);

            RefreshPic();

            // кнопки снизу картинки

            panel3.Location = new Point(panel3.Location.X, Pic.Height + 40);
            panel3.Size = new Size(Pic.Size.Width, panel3.Size.Height);
            buttonSaveData.Location = new Point(panel3.Size.Width / 2 - buttonSaveData.Size.Width / 2, buttonSaveData.Location.Y);
            
        }

        
    }
}
