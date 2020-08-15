public static class HslRgbUtils
    {
        public static Color HslToRgb(double h, double sl, double l)
        {

            double v;
            double r, g, b;
            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            return Color.FromArgb(Convert.ToByte(r * 255.0f),Convert.ToByte(g * 255.0f),Convert.ToByte(b * 255.0f));

        }

        public static void RgbToHsl(Color rgb, out double h, out double s, out double l)
        {
            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;
            double v;
            double m;
            double vm;
            double r2, g2, b2;

            h = 0; // default to black
            s = 0;
            l = 0;
            v = Math.Max(r, g);
            v = Math.Max(v, b);
            m = Math.Min(r, g);
            m = Math.Min(m, b);

            l = (m + v) / 2.0;
            if (l <= 0.0)
            {
                return;
            }
            vm = v - m;
            s = vm;
            if (s > 0.0)
            {
                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
            }
            else
            {
                return;
            }
            r2 = (v - r) / vm;
            g2 = (v - g) / vm;
            b2 = (v - b) / vm;
            if (r == v)
            {
                h = (g == m ? 5.0 + b2 : 1.0 - g2);
            }
            else if (g == v)
            {
                h = (b == m ? 1.0 + r2 : 3.0 - b2);
            }
            else
            {
                h = (r == m ? 3.0 + g2 : 5.0 - r2);
            }
            h /= 6.0;
        }
    }
	
	public class LAB
	{
		public double L;
        public double A;
        public double B; 
	}
	
	public class XYZ
	{
		public double X;
        public double Y;
        public double Z;	
	}
	
	public class HSLColor
	{
	    public float Hue;
	    public float Saturation;
	    public float Luminosity;
	
	    public HSLColor(float H, float S, float L)
	    {
	        Hue = H;
	        Saturation = S;
	        Luminosity = L;
	    }
	}
	
	public class CmykColor
	{
		public float C { get; set; }
		public float M { get; set; }
		public float Y { get; set; }
		public float K { get; set; }
		
		public CmykColor(float c, float m, float y, float k) {
			this.C = c;
			this.M = m;
			this.Y = y;
			this.K = k;
		}
	}
	
	public static class ConverterColorsApi
	{
		// Например new byte[]{70,70,48,48,70,70}
		public static string BytesToHex(byte[] bytes) {
			char[] chars = bytes.Select(x => (char)x).ToArray();
			string hex = new string(chars);
			return hex;
		}
		
		public static int[] BytesToRGB(byte[] bytes) {
			var str1 = new string(new char[] { (char)bytes[0], (char)bytes[1] });
			var str2 = new string(new char[] { (char)bytes[2], (char)bytes[3] });
			var str3 = new string(new char[] { (char)bytes[4], (char)bytes[5] });
			 
			int r = Convert.ToInt32(str1, 16);
			int g = Convert.ToInt32(str2, 16);
			int b = Convert.ToInt32(str3, 16);
			
			return new int[] { r, g, b };
		}
		
		public static Color CmykToRgb(float c, float m, float y, float k)
		{
		  int r;
		  int g;
		  int b;
		
		  r = Convert.ToInt32(255 * (1 - c) * (1 - k));
		  g = Convert.ToInt32(255 * (1 - m) * (1 - k));
		  b = Convert.ToInt32(255 * (1 - y) * (1 - k));
		
		  return Color.FromArgb(r, g, b);
		}
		
		public static CmykColor RgbToCmyk(int r, int g, int b)
		{
		  float c;
		  float m;
		  float y;
		  float k;
		  float rf;
		  float gf;
		  float bf;
		
		  rf = r / 255F;
		  gf = g / 255F;
		  bf = b / 255F;
		
		  k = ClampCmyk(1 - Math.Max(Math.Max(rf, gf), bf));
		  c = ClampCmyk((1 - rf - k) / (1 - k));
		  m = ClampCmyk((1 - gf - k) / (1 - k));
		  y = ClampCmyk((1 - bf - k) / (1 - k));
		
		  return new CmykColor(c, m, y, k);
		}
		
		private static float ClampCmyk(float value)
		{
		  if (value < 0 || float.IsNaN(value))
		  {
		    value = 0;
		  }
		
		  return value;
		}
	
		public static Color HslToRgb(double h, double s, double l) {
			return HslRgbUtils.HslToRgb(h,s,l);
		}
		
		public static HSLColor RgbToHsl(Color color) {
			double h = 0;
			double s = 0;
			double l = 0;
			
			HslRgbUtils.RgbToHsl
				(color, out h, out s, out l);
			
			return new HSLColor((float)h,(float)s,(float)l);
		}
	
		public static Color XyzToRgb(XYZ xyz) {
			xyz.X = xyz.X / 100;
            xyz.Y = xyz.Y / 100;
            xyz.Z = xyz.Z / 100;
 
            double rFloat = xyz.X * 3.2406 + xyz.Y * -1.5372 + xyz.Z * -0.4986;
            double gFloat = xyz.X * -0.9689 + xyz.Y * 1.8758 + xyz.Z * 0.0415;
            double bFloat = xyz.X * 0.0557 + xyz.Y * 0.2040 + xyz.Z * 1.0570;

 
            rFloat = rFloat > 0.0031308 ? 1.055 * Math.Pow(rFloat, 0.41666) - 0.055 : 12.92 * rFloat;
            gFloat = gFloat > 0.0031308 ? 1.055 * Math.Pow(gFloat, 0.41666) - 0.055 : 12.92 * gFloat;
            bFloat = bFloat > 0.0031308 ? 1.055 * Math.Pow(bFloat, 0.41666) - 0.055 : 12.92 * bFloat;
 
            return Color.FromArgb((int)(rFloat * 255), (int)(gFloat * 255), (int)(bFloat * 255));
		}
		
		public static XYZ RgbToXyz(Color c) {
			XYZ xyz = new XYZ();
 
            double rFloat = c.R / 255;      // Нормализация цветов RGB
            double gFloat = c.G / 255;
            double bFloat = c.B / 255;
 
            /* Преобразование значений RGB в пространство цветов sRGB */
            rFloat = rFloat > 0.04045 ? Math.Pow((rFloat + 0.055) / 1.055, 2.2) : rFloat / 12.92;
            gFloat = gFloat > 0.04045 ? Math.Pow((gFloat + 0.055) / 1.055, 2.2) : gFloat / 12.92;
            bFloat = bFloat > 0.04045 ? Math.Pow((bFloat + 0.055) / 1.055, 2.2) : bFloat / 12.92;
 
            /* Вычисление XYZ с использовением коррекции D65 */
            xyz.X = rFloat * 0.4124 + gFloat * 0.3576 + bFloat * 0.1805;
            xyz.Y = rFloat * 0.2126 + gFloat * 0.7152 + bFloat * 0.0722;
            xyz.Z = rFloat * 0.0193 + gFloat * 0.1192 + bFloat * 0.9505;
 
            return xyz;
		}
		
		public static XYZ LabToXyz(LAB lab) {
			XYZ xyz = new XYZ();
 
            xyz.Y = (lab.L + 16.0) / 116.0;
            xyz.X = lab.A / 500.0 + xyz.Y;
            xyz.Z = xyz.Y - lab.B / 200.0;
 
            xyz.X = oXYZ(xyz.X);
            xyz.Y = oXYZ(xyz.Y);
            xyz.Z = oXYZ(xyz.Z);
 
            xyz.X = 95.047 * xyz.X;
            xyz.Y = 100 * xyz.Y;
            xyz.Z = 108.883 * xyz.Z;
 
            return xyz;
		}
		
		public static LAB XyzToLab(XYZ xyz) {
			LAB lab = new LAB();
 
            double tmp = fXYZ(xyz.Y);                             // Чтобы три раза не считать, а только один
 
            lab.L = 116 * tmp - 16;
            lab.A = 500 * (fXYZ(xyz.X / 0.9505) - tmp);
            lab.B = 200 * (tmp - fXYZ(xyz.Z / 1.089));
 
            return lab;
		}
		
		private static double fXYZ(double tmp)
        {
            return tmp > 0.008856 ? Math.Pow(tmp, 1.0 / 3.0) : (7.787 * tmp + 16 / 116);
        }
		
		private static double oXYZ(double tmp)
        {
            return Math.Pow(tmp, 3) > 0.008856 ? Math.Pow(tmp, 3) : (tmp - 16.0 / 116.0) / 7.787;
        }
		
		public static LAB RgbToLab(Color c) {
			return XyzToLab(RgbToXyz(c));
		}
		
		public static Color LabToRgb(LAB lab) {
			return XyzToRgb(LabToXyz(lab));
		}
	}
