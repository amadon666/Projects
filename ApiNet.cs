using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using System.Linq;
namespace ApiNet
{
    public static class NetUtils
	{
		[DllImport("Advapi32.dll", EntryPoint="GetUserName", 
        ExactSpelling=false, SetLastError=true)]
		
		// Возвращает имя пользователя компьютера 
		public static extern bool GetUserName(
	    [MarshalAs(UnmanagedType.LPArray)] byte[] lpBuffer,
	    [MarshalAs(UnmanagedType.LPArray)] Int32[] nSize );
		
		[Obsolete]
		public static string GetIPAddress()
		{
			String host = Dns.GetHostName();
			return Dns.GetHostByName(host).AddressList[0].ToString();
		}
		
		public static void GetIPAddresses()
		{
			String key = @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces";
    
		    using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(key)) {
		      if (rk == null) return;
		      
		      (from sub in rk.GetSubKeyNames()
		        let itm = rk.OpenSubKey(sub) select new {ip = itm.GetValue("DhcpIpAddress")}
		      )
		      .ToList()
		      .FindAll(i => i.ip != null)
		      .ForEach(i => Console.WriteLine(i.ip.ToString() != "0.0.0.0" ? i.ip : "127.0.0.1"));
			}
		}
		
		/// Позволяет узнать внешний(глобальный) ip-адрес компьютера в сети
		public static string GetPublicIPFromIpify()
		{
			return new WebClient().DownloadString("https://api.ipify.org");
		}
		
		/* Сервисы чтобы узнать публичный IP-адрес
			https://ipinfo.io/ip/
			https://api.ipify.org/
			https://icanhazip.com/
			https://checkip.amazonaws.com/
			https://wtfismyip.com/text
		*/
		
		public static string GetPublicIPFromDyndns()
	    {
	        WebRequest req = WebRequest.Create("http://checkip.dyndns.org");
	        WebResponse resp = req.GetResponse();
	        StreamReader sr = new StreamReader(resp.GetResponseStream());
	        string response = sr.ReadToEnd().Trim();
	        string[] a = response.Split(':');
	        string a2 = a[1].Substring(1);
	        string[] a3 = a2.Split('<');
	        string a4 = a3[0];
	        return a4;
		}
		
		public static string GetPublicIPFromIPInfoIO()
		{
			return new WebClient().DownloadString("https://ipinfo.io/ip");
		}
		
		public static string GetPublicIpFromIfConfig()
		{
		    var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");
		    request.UserAgent = "curl"; // this will tell the server to return the information as if the request was made by the linux "curl" command
		
		    string publicIPAddress;
		
		    request.Method = "GET";
		    using (WebResponse response = request.GetResponse())
		    {
		        using (var reader = new StreamReader(response.GetResponseStream()))
		        {
		            publicIPAddress = reader.ReadToEnd();
		        }
		    }
		
		    return publicIPAddress.Replace("\n", "");
		}
		  
		public static string GetPublicIpFromIcanhazip()
		{
			return new WebClient().DownloadString("http://icanhazip.com");
		}
		
		public static string GetExternalIPAddress()
        {
            string result = string.Empty;
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers["User-Agent"] =
                    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; " +
                    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                    try
                    {
                        byte[] arr = client.DownloadData("http://checkip.amazonaws.com/");

                        string response = Encoding.UTF8.GetString(arr);
                        result = response.Trim();
                    }
                    catch (WebException)
                    {                       
                    }
                }
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://ipinfo.io/ip").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://api.ipify.org").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://icanhazip.com").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://wtfismyip.com/text").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("http://bot.whatismyipaddress.com/").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    string url = "http://checkip.dyndns.org";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    string[] a = response.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    result = a3[0];
                }
                catch (Exception)
                {
                }
            }

            return result;
        }
	
		// Является ли ip приватным или публичным(глобальным)
    	public static bool IsLocalIp(IPAddress ip) {
		    var ipParts = ip.ToString().Split(new [] { "." }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
		
		    return (ipParts[0] == 192 && ipParts[1] == 168) 
		        || (ipParts[0] == 172 && ipParts[1] >= 16 && ipParts[1] <= 31) 
		        ||  ipParts[0] == 10;
		}
		
		public static string GetWorldIP()
		{
		    String url = "http://bot.whatismyipaddress.com/";
		    String result = null;
		
		    try
		    {
		        WebClient client = new WebClient();
		        result = client.DownloadString(url);
		        return result;
		    }
		    catch (Exception ex) { return "127.0.0.1"; }
		}
    
		public static IPAddress[] GetIPAddressesFromSite(string sitename)
		{
			IPHostEntry host = Dns.GetHostEntry(sitename);
			return host.AddressList;
		}
    }
    
    public class CheckSum
    {
    	public static string OnlyDigits(string str)
    	{
    		return string.Join("", str.Where(c => char.IsDigit(c)));
    	}
    	
    	public static Int32 SNILSControlCalc(string snils)
    	{
    		string workSnils = OnlyDigits(snils);
    		
    		if (workSnils.Length != 9 && workSnils.Length != 11)
    		{
    			throw new Exception(String.Format("Incorrect SNILS number. {0} digits! (it can only be 9 or 11 digits!)", workSnils.Length));
    		}
    		if(workSnils.Length == 11){
                workSnils = workSnils.Substring(0,9);
            }
            
            Int32 totalSum = 0;
            for(Int32 i = workSnils.Length-1, j=0;i>=0;i--, j++){
                Int32 digit = Int32.Parse(workSnils[i].ToString());
                totalSum += digit*(j+1);
            }
            
            return SNILSCheckControlSum(totalSum);
    		
    	}
    	
    	private static Int32 SNILSCheckControlSum(Int32 _controlSum){
            Int32 result;
            if(_controlSum < 100){
                result = _controlSum;
            }
            else if(_controlSum <= 101){
                result = 0;
            }
            else{
                Int32 balance = _controlSum%101;
                result = SNILSCheckControlSum(balance);
            }
            return result;
        }
    	
    	public static Boolean SNILSValidate(String snils){
            String workSnils = OnlyDigits(snils);
            Boolean result = false;
            
            if(workSnils.Length == 9){
                if(SNILSControlCalc(workSnils) > -1){
                    result = true;
                }
            }
            else if(workSnils.Length == 11){
                Int32 controlSum = SNILSControlCalc(workSnils);
                Int32 strControlSum = Int32.Parse(workSnils.Substring(9, 2));
                if(controlSum == strControlSum){
                    result = true;
                }
            }
            else{
                throw new Exception(String.Format("Incorrect SNILS number. {0} digits! (it can only be 9 or 11 digits!)", workSnils.Length));
            }
            
            return result;
        }
    }
	
    public class CheckINN
    {
    	/// <summary>
        /// Проверка корректности ИНН, представленного в виде строки
        /// За основу взят алгоритм http://www.rsdn.ru/Forum/Message.aspx?mid=647880
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsINN(string value)
        {
            if (value.Length == 11)
            {
                if (value[0] != 'F')
                    return false;
                else
                    value = value.Remove(0, 1);
            }
            
            // должно быть 10 или 12 цифр
            if (!(value.Length == 10 || value.Length == 12))
                return false;
            else
            {
                try 
                { 
                    return IsINN(long.Parse(value)); 
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Проверка корректности ИНН, представленного в виде числа
        /// За основу взят алгоритм http://www.rsdn.ru/Forum/Message.aspx?mid=647880
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsINN(long value)
        {
            // должно быть 10 или 12 цифр
            if (value < 1000000000 || value >= 1000000000000)
                return false;

            int digits = (int)Math.Log10(value) + 1;
            if (!(digits == 10 || digits == 12))
                return false;

            // вычисляем контрольную сумму
            string s = value.ToString("D" + digits.ToString());
            int[] factors = digits == 10 ? arrMul10 : arrMul122;

        startCheck:

            long sum = 0;
            for (int i = 0; i < factors.Length; i++)
                sum += byte.Parse(s[i].ToString()) * factors[i];
            sum %= 11;
            sum %= 10;
            if (sum != byte.Parse(s[factors.Length].ToString()))
                return false;
            else if (digits == 12)
            {
                // используется маленький трюк:
                // запускается повторная проверка, начиная с метки startCheck,
                // но с другими коэффициентами, а чтобы исключить повторный вход 
                // в эту ветку, сбрасываем digits
                factors = arrMul121;
                digits = 0;
                goto startCheck;
            }
            else
                return true;
        }

        #region Коффициенты для проверки ИНН (метод IsINN)

        static readonly int[] arrMul10 = {2, 4, 10, 3, 5, 9, 4, 6, 8};
        static readonly int[] arrMul121 = {7, 2, 4, 10, 3, 5, 9, 4, 6, 8};
        static readonly int[] arrMul122 = {3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8};

        #endregion Коффициенты для проверки ИНН (метод IsINN)
    }
    
    /*
    private static byte[] NetworkToHostOrder (byte[] array, int offset, int length)
{
    return array.Skip (offset).Take (length).Reverse ().ToArray ();
}

int foo = BitConverter.ToInt64 (NetworkToHostOrder (queue, 14, 8), 0);
    */
    
	public class Program
	{
		public static void Main(string[] args)
		{
			// TEST 1: GetUserName()
//			byte[] str=new byte[256];
//		    Int32[] len=new Int32[1];
//		    len[0]=256;
//		    NetUtils.GetUserName(str,len);
//		    Console.WriteLine(Encoding.ASCII.GetString(str));
    
//			string ips = NetUtils.GetExternalIPAddress();
//			IPAddress ip = IPAddress.Parse(ips);
//			Console.WriteLine(NetUtils.IsLocalIp(ip));
			
			//Console.WriteLine(CheckSum.SNILSValidate("234-322-191"));
			//Console.WriteLine(CheckINN.IsINN("780204893183"));
			
			foreach (IPAddress ip in NetUtils.GetIPAddressesFromSite("www.microsoft.com"))
    			Console.WriteLine(ip.ToString());
			
			foreach (IPAddress ip in NetUtils.GetIPAddressesFromSite("google.com"))
    			Console.WriteLine(ip.ToString());
			
			Console.ReadKey(true);
		}
	}
}
