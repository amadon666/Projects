using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace WeatherApp
{
	public class WeatherData
    {
        public double Temperature;
        public double Pressure;
        public double Humidity;
        public double PrecipitationIntensity;
        public double CloudsCoverage;
        public double PrecipitationSolidity;
        public double StormChance;
        public double WindSpeed;
        public double WindDirection;
        public DateTime Date;
 
        public void Read(string source)
        {
            string[] tempers = source.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempers.Length; i++)
            {
                Temperature = double.Parse(tempers[0]);
                Pressure = double.Parse(tempers[1]);
                Humidity = double.Parse(tempers[2]);
                PrecipitationIntensity = double.Parse(tempers[3]);
                CloudsCoverage = double.Parse(tempers[4]);
                PrecipitationSolidity= double.Parse(tempers[5]);
                StormChance = double.Parse(tempers[6]);
                WindSpeed = double.Parse(tempers[7]);
                WindDirection = double.Parse(tempers[8]);
            }
        }
        
        public override string  ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", new object[]{
                Temperature.ToString("0.000"),Pressure.ToString("0.000"),Humidity.ToString("0.000"),
                PrecipitationIntensity.ToString("0.000"),CloudsCoverage.ToString("0.000"),PrecipitationSolidity.ToString("0.000"),
                StormChance.ToString("0.000"),WindSpeed.ToString("0.000"),WindDirection.ToString("0.000")});
        }
    }
	
	class Program
	{
		public static string getHumidity(string data, string[] strs) {
			return new Regex(@"\d+?%").Match(strs[583]).Value;
		}
		
		public static string getDropPoint(string data, string[] strs) {
			return new Regex(@"\d+°").Match(strs[587]).Value;
		}
		
		public static string getCountCoulds(string data, string[] strs) {
			return new Regex(@"(?<=>)(.*)(?=<)").Match(strs[567]).Value;
		}
		
		public static string getWind(string data, string[] strs) {
			string str = new Regex(@"(?<=>)(.*)(?=<)").Match(strs[578]).Value.Replace(" ", "").Replace("&nbsp;","");
			return (new Regex(@"\w+,").Match(str).Value) + (new Regex(@"\d+м/с").Match(str).Value);
		}
		
		
		public static void Main(string[] args)
		{
//			string data, temp, osadki;
//			string clouds;
//			string measure;
//			string pressure;
//			string visibility;
//			WebRequest request;
//                request = WebRequest.Create(@"http://www.meteoservice.ru/weather/now/moskva");
//                //request = WebRequest.Create(@"https://www.meteoservice.ru/weather/now/ahtubinsk");
//                using (var response = request.GetResponse())
//                {
//                    using (var stream = response.GetResponseStream())
//                    using (var reader = new StreamReader(stream))
//                    {
//                        data = reader.ReadToEnd();
//                        measure = new Regex(@"Измерено в \d+?:\d\d").Match(data).Value;
//                        temp = new Regex(@"(?<=<span class=""value "">)(.*)(?=</span>)").Match(data).Value;
//                        osadki = new Regex(@"(?<=<p class=""margin-bottom-0"">)(.*)(?=</p>)").Match(data).Value;
//                        clouds = new Regex(@"(?<=<div class=""font-smaller"">)(.*)(?=</div>)").Match(data).Value;
//                        
//                        pressure = new Regex(@"(?<=<div class=""h6 nospace-bottom"">)(.*)(?=</div>)").Match(data).Value;
//                        pressure = new Regex(@"\d+").Match(pressure).Value;
//                        
//                        visibility = new Regex(@"(?<=<div class=""h5 margin-bottom-0"">)(.*)(?=</div>)").Match(data).Value;
//                        
//                     }
//                }
//                
//                string[] strs = data.Split('\n');
//                
//                //Console.WriteLine();
//                //Console.WriteLine(measure);
//                Console.WriteLine("Точка росы: " + getDropPoint(data, strs));
//                Console.WriteLine("Влажность: " + getHumidity(data, strs));
//                Console.WriteLine("Атмосферное давление(барометр): " + pressure + "мм рт.ст."); 
//                Console.WriteLine("Кол-во облаков: " + getCountCoulds(data, strs));
//                Console.WriteLine("Видимость: " + visibility);
//                Console.WriteLine("\nТемпература воздуха: " + temp + "\n" + "Осадки: " + osadki);
//                Console.WriteLine("Виды облаков: " + clouds);
//                Console.WriteLine("Ветер: " + getWind(data, strs));
				
string htmlCode = @"<html>
<head>
	<style></style>
</head>
<body>
	<div></div>
	<div></div>
	<div></div>
	<div>
		<label>TEXT SECURY</label
	</div>
	<div></div>
</body>
</html>";
				string nodeValue;
				HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
	            doc.LoadHtml(htmlCode);
	            HtmlNode node = doc.DocumentNode.SelectSingleNode("/html/body/div[4]/label");
	            nodeValue = (node.InnerText);
	            Console.WriteLine(nodeValue);


                        Console.ReadLine();
		}
	}
}
