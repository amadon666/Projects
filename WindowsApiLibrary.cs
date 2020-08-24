using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security;
using System.Text;
using System.Diagnostics.Eventing;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
namespace ConsoleApp3
{
    internal static class ExternDll
    {
        internal const string Activeds = "activeds.dll";
        internal const string Advapi32 = "advapi32.dll";
        internal const string Comctl32 = "comctl32.dll";
        internal const string Comdlg32 = "comdlg32.dll";
        internal const string Gdi32 = "gdi32.dll";
        internal const string Gdiplus = "gdiplus.dll";
        internal const string Hhctrl = "hhctrl.ocx";
        internal const string Imm32 = "imm32.dll";
        internal const string Kernel32 = "kernel32.dll";
        internal const string Loadperf = "Loadperf.dll";
        internal const string Mscoree = "mscoree.dll";
        internal const string Clr = "clr.dll";
        internal const string Msi = "msi.dll";
        internal const string Mqrt = "mqrt.dll";
        internal const string Ntdll = "ntdll.dll";
        internal const string Ole32 = "ole32.dll";
        internal const string Oleacc = "oleacc.dll";
        internal const string Oleaut32 = "oleaut32.dll";
        internal const string Olepro32 = "olepro32.dll";
        internal const string PerfCounter = "perfcounter.dll";
        internal const string Powrprof = "Powrprof.dll";
        internal const string Psapi = "psapi.dll";
        internal const string Shell32 = "shell32.dll";
        internal const string User32 = "user32.dll";
        internal const string Uxtheme = "uxtheme.dll";
        internal const string WinMM = "winmm.dll";
        internal const string Winspool = "winspool.drv";
        internal const string Wtsapi32 = "wtsapi32.dll";
        internal const string Version = "version.dll";
        internal const string Vsassert = "vsassert.dll";
        internal const string Fxassert = "Fxassert.dll";
        internal const string Shlwapi = "shlwapi.dll";
        internal const string Crypt32 = "crypt32.dll";
        internal const string ShCore = "SHCore.dll";
        internal const string Wldp = "wldp.dll";
        internal const string Odbc32 = "odbc32.dll";
        internal const string SNI = "System.Data.dll";
        internal const string OciDll = "oci.dll";
        internal const string OraMtsDll = "oramts.dll";
    }
    /// <summary>
    /// API для работы с курсором
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal sealed class WinCaret
    {
        private IntPtr controlHandle;
        //Создает новую фигуру для системного курсора и назначает владение курсором указанному окну. 
        //Фигура курсора может быть линией, блоком или растровым изображением.
        [DllImport("User32.dll")]
        private static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);
        //Перемещает курсор в указанные координаты.
        [DllImport("User32.dll")]
        private static extern bool SetCaretPos(int x, int y);
        //Уничтожает текущую форму курсора, освобождает курсор от окна и удаляет курсор с экрана.
        [DllImport("User32.dll")]
        private static extern bool DestroyCaret();
        //Делает курсор видимым на экране в текущем положении курсора. Когда курсор становится видимым, он начинает мигать автоматически.
        [DllImport("User32.dll")]
        private static extern bool ShowCaret(IntPtr hWnd);
        //Удаляет курсор с экрана. Скрытие курсора не разрушает его текущую форму и не делает недействительной точку вставки.
        [DllImport("User32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);

        public WinCaret(IntPtr ownerHandle)
        {
            controlHandle = ownerHandle;
        }
        //Создает новую фигуру для системного курсора и назначает владение курсором указанному окну. 
        //Фигура курсора может быть линией, блоком или растровым изображением.
        public bool Create(int width, int height)
        {
            return CreateCaret(controlHandle, 0, width, height);
        }
        //Удаляет курсор с экрана. Скрытие курсора не разрушает его текущую форму и не делает недействительной точку вставки.
        public void Hide()
        {
            HideCaret(controlHandle);
        }
        //Делает курсор видимым на экране в текущем положении курсора. Когда курсор становится видимым, он начинает мигать автоматически.
        public void Show()
        {
            ShowCaret(controlHandle);
        }
        //Перемещает курсор в указанные координаты.
        public bool SetPosition(int x, int y)
        {
            return SetCaretPos(x, y);
        }
        //Уничтожает текущую форму курсора, освобождает курсор от окна и удаляет курсор с экрана.
        public void Destroy()
        {
            DestroyCaret();
        }
    }
    /// <summary>
    /// API для работы со временем
    /// </summary>
    internal sealed class TimeApi
    {
        internal const int TIME_ZONE_ID_INVALID = -1;
        internal const int TIME_ZONE_ID_UNKNOWN = 0;
        internal const int TIME_ZONE_ID_STANDARD = 1;
        internal const int TIME_ZONE_ID_DAYLIGHT = 2;
        internal const int MAX_PATH = 260;
        internal const int MUI_LANGUAGE_ID = 4;
        internal const int MUI_LANGUAGE_NAME = 8;
        internal const int MUI_PREFERRED_UI_LANGUAGES = 16;
        internal const int MUI_INSTALLED_LANGUAGES = 32;
        internal const int MUI_ALL_LANGUAGES = 64;
        internal const int MUI_LANG_NEUTRAL_PE_FILE = 256;
        internal const int MUI_NON_LANG_NEUTRAL_FILE = 512;
        internal const int LOAD_LIBRARY_AS_DATAFILE = 2;
        internal const int LOAD_STRING_MAX_LENGTH = 500;

        private TimeApi() { }
        //Задает дату и время, используя отдельные элементы для месяца, Дня, года, дня недели, часа, минуты, секунды и миллисекунды. Время указывается либо в формате UTC, либо по местному времени, в зависимости от вызываемой функции.
        internal struct SystemTime
        {
            [MarshalAs(UnmanagedType.U2)]
            public short Year;
            [MarshalAs(UnmanagedType.U2)]
            public short Month;
            [MarshalAs(UnmanagedType.U2)]
            public short DayOfWeek;
            [MarshalAs(UnmanagedType.U2)]
            public short Day;
            [MarshalAs(UnmanagedType.U2)]
            public short Hour;
            [MarshalAs(UnmanagedType.U2)]
            public short Minute;
            [MarshalAs(UnmanagedType.U2)]
            public short Second;
            [MarshalAs(UnmanagedType.U2)]
            public short Milliseconds;
        }
        //Задает параметры для часового пояса.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TimeZoneInformation
        {
            [MarshalAs(UnmanagedType.I4)]
            public int Bias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string StandardName;
            public SystemTime StandardDate;
            [MarshalAs(UnmanagedType.I4)]
            public int StandardBias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DaylightName;
            public SystemTime DaylightDate;
            [MarshalAs(UnmanagedType.I4)]
            public int DaylightBias;

            public TimeZoneInformation(DynamicTimeZoneInformation dtzi)
            {
                Bias = dtzi.Bias;
                StandardName = dtzi.StandardName;
                StandardDate = dtzi.StandardDate;
                StandardBias = dtzi.StandardBias;
                DaylightName = dtzi.DaylightName;
                DaylightDate = dtzi.DaylightDate;
                DaylightBias = dtzi.DaylightBias;
            }
        }
        //Задает параметры для часового пояса и динамического летнего времени.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DynamicTimeZoneInformation
        {
            [MarshalAs(UnmanagedType.I4)]
            public int Bias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string StandardName;
            public SystemTime StandardDate;
            [MarshalAs(UnmanagedType.I4)]
            public int StandardBias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DaylightName;
            public SystemTime DaylightDate;
            [MarshalAs(UnmanagedType.I4)]
            public int DaylightBias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string TimeZoneKeyName;
        }
        internal struct RegistryTimeZoneInformation
        {
            [MarshalAs(UnmanagedType.I4)]
            public int Bias;
            [MarshalAs(UnmanagedType.I4)]
            public int StandardBias;
            [MarshalAs(UnmanagedType.I4)]
            public int DaylightBias;
            public SystemTime StandardDate;
            public SystemTime DaylightDate;

            public RegistryTimeZoneInformation(TimeZoneInformation tzi)
            {
                Bias = tzi.Bias;
                StandardDate = tzi.StandardDate;
                StandardBias = tzi.StandardBias;
                DaylightDate = tzi.DaylightDate;
                DaylightBias = tzi.DaylightBias;
            }

            public RegistryTimeZoneInformation(byte[] bytes)
            {
                if (bytes == null || bytes.Length != 44)
                    throw new ArgumentException(nameof(bytes));
                Bias = BitConverter.ToInt32(bytes, 0);
                StandardBias = BitConverter.ToInt32(bytes, 4);
                DaylightBias = BitConverter.ToInt32(bytes, 8);
                StandardDate.Year = BitConverter.ToInt16(bytes, 12);
                StandardDate.Month = BitConverter.ToInt16(bytes, 14);
                StandardDate.DayOfWeek = BitConverter.ToInt16(bytes, 16);
                StandardDate.Day = BitConverter.ToInt16(bytes, 18);
                StandardDate.Hour = BitConverter.ToInt16(bytes, 20);
                StandardDate.Minute = BitConverter.ToInt16(bytes, 22);
                StandardDate.Second = BitConverter.ToInt16(bytes, 24);
                StandardDate.Milliseconds = BitConverter.ToInt16(bytes, 26);
                DaylightDate.Year = BitConverter.ToInt16(bytes, 28);
                DaylightDate.Month = BitConverter.ToInt16(bytes, 30);
                DaylightDate.DayOfWeek = BitConverter.ToInt16(bytes, 32);
                DaylightDate.Day = BitConverter.ToInt16(bytes, 34);
                DaylightDate.Hour = BitConverter.ToInt16(bytes, 36);
                DaylightDate.Minute = BitConverter.ToInt16(bytes, 38);
                DaylightDate.Second = BitConverter.ToInt16(bytes, 40);
                DaylightDate.Milliseconds = BitConverter.ToInt16(bytes, 42);
            }
        }
    }
    /// <summary>
    /// API для работы со звуком
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class SoundApi
    {
        //Воспроизведение звука сигнала. Форма сигнала для каждого типа звука определяется записью в реестре.
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool MessageBeep(int type);
        //Воспроизводит звук из указанного файла
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        internal static extern bool PlaySound([MarshalAs(UnmanagedType.LPWStr)] string soundName, IntPtr hmod, int soundFlags);
        //Воспроизводит звук из указанного файла
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        internal static extern bool PlaySound(byte[] soundName, IntPtr hmod, int soundFlags);
        //Функция mmioOpen открывает файл для небуферизованного или буферизованного ввода-вывода; создает файл; удаляет файл; или проверяет, существует ли файл. 
        //Файл может быть стандартным файлом, файлом памяти или элементом пользовательской системы хранения. Дескриптор, возвращаемый mmioOpen, не является стандартным дескриптором файла; не используйте его с любыми функциями ввода-вывода файлов, кроме функций ввода-вывода мультимедийных файлов.
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr MmioOpen(string fileName, IntPtr not_used, int flags);
        //Функция чтения mmio считывает указанное число байт из файла, открытого с помощью функции mmioOpen.
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        internal static extern int MmioRead(IntPtr hMIO, [MarshalAs(UnmanagedType.LPArray)] byte[] wf, int cch);
        //Функция mmioClose закрывает файл, открытый с помощью функции mmioOpen.
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        internal static extern int MmioClose(IntPtr hMIO, int flags);
    }
    /// <summary>
    /// API DWM
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal class DwmApi
    {
        public const int DWM_BB_BLURREGION = 2;
        public const int DWM_BB_ENABLE = 1;
        public const int DWM_BB_TRANSITIONONMAXIMIZED = 4;
        public const string DWM_COMPOSED_EVENT_BASE_NAME = "DwmComposedEvent_";
        public const string DWM_COMPOSED_EVENT_NAME_FORMAT = "%s%d";
        public const int DWM_COMPOSED_EVENT_NAME_MAX_LENGTH = 64;
        public const int DWM_FRAME_DURATION_DEFAULT = -1;
        public const int DWM_TNP_OPACITY = 4;
        public const int DWM_TNP_RECTDESTINATION = 1;
        public const int DWM_TNP_RECTSOURCE = 2;
        public const int DWM_TNP_SOURCECLIENTAREAONLY = 16;
        public const int DWM_TNP_VISIBLE = 8;
        public const int WM_DWMCOMPOSITIONCHANGED = 798;
        public static uint WTNCA_NODRAWCAPTION;
        public static uint WTNCA_NODRAWICON;
        public static uint WTNCA_NOSYSMENU;
        public static uint WTNCA_NOMIRRORHELP;
        public static readonly bool DwmApiAvailable;
        //Процедура окна по умолчанию для Desktop Window Manager (DWM) хит тестирования в пределах неклиентской области.
        //Также необходимо убедиться, что DwmDefWindowProc вызывается для сообщения WM_NCMOUSELEAVE.Если DwmDefWindowProc не вызывается для сообщения WM_NCMOUSELEAVE, DWM не удаляет выделение из кнопок развернуть, свернуть и закрыть, когда курсор покидает окно.
        [DllImport("dwmapi.dll")]
        public static extern int DwmDefWindowProc(
          IntPtr hwnd,
          int msg,
          IntPtr wParam,
          IntPtr lParam,
          ref IntPtr result);
        //Включает или отключает композицию Desktop Window Manager (DWM).
        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableComposition(int fEnable);
        //Уведомляет диспетчер окон рабочего стола (DWM) о включении или отключении планирования Службы планировщика классов мультимедиа (MMCSS) во время работы вызывающего процесса.
        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableMMCSS(int fEnableMMCSS);
        //Расширяет рамку окна в клиентскую область.
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hdc, ref MARGINS marInset);
        //Возвращает текущий цвет, используемый для композиции стекла Desktop Window Manager (DWM). Это значение основано на текущей цветовой схеме и может быть изменено пользователем. 
        //Приложения могут прослушивать изменения цвета, обрабатывая уведомление WM_DWM COLORIZATIONCOLOR CHANGED.
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetColorizationColor(ref int pcrColorization, ref int pfOpaqueBlend);
        //Извлекает текущую информацию о времени композиции для указанного окна.
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetCompositionTimingInfo(
          IntPtr hwnd,
          ref DWM_TIMING_INFO pTimingInfo);
        //Извлекает текущее значение указанного атрибута Desktop Window Manager (DWM), примененного к окну.
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(
          IntPtr hwnd,
          int dwAttribute,
          IntPtr pvAttribute,
          int cbAttribute);
        //Получает значение, указывающее, включена ли композиция Desktop Window Manager (DWM). 
        //Приложения на компьютерах под управлением Windows 7 или более ранних версий могут прослушивать изменения состояния композиции, обрабатывая уведомление WM_DWMCOMPOSITIONCHANGED.
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        //Изменяет количество обновлений монитора, через которые будет отображаться предыдущий кадр.
        //DwmModifyPreviousDxFrameDuration больше не поддерживается.Начиная с Windows 8.1, вызовы DwmModifyPreviousDxFrameDuration всегда возвращают E_NOTIMPL.        
        [DllImport("dwmapi.dll")]
        public static extern int DwmModifyPreviousDxFrameDuration(
          IntPtr hwnd,
          int cRefreshes,
          int fRelative);
        //Извлекает исходный размер миниатюры Desktop Window Manager (DWM).
        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr hThumbnail, ref Size pSize);
        //Создает связь миниатюр диспетчера окон рабочего стола (DWM)между конечным и исходным окнами.
        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(
          IntPtr hwndDestination,
          IntPtr hwndSource,
          ref Size pMinimizedSize,
          ref IntPtr phThumbnailId);
        //Задает количество обновлений монитора, через которые будет отображаться представленный кадр.
        //Длительность кадра Dwm Set Dx больше не поддерживается.Начиная с Windows 8.1, звонки для МДВ набор кадрирования DX всегда продолжительность возвращать e_notimpl.
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetDxFrameDuration(IntPtr hwnd, int cRefreshes);
        //Задает текущие параметры для композиции кадра.
        //DwmSetPresentParameters больше не поддерживается.Начиная с Windows 8.1, вызовы DwmSetPresentParameters всегда возвращают E_NOTIMPL.       
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetPresentParameters(
          IntPtr hwnd,
          ref DWM_PRESENT_PARAMETERS pPresentParams);
        //Задает значение неклиентских атрибутов отображения Desktop Window Manager (DWM) для окна.
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(
          IntPtr hwnd,
          int dwAttribute,
          IntPtr pvAttribute,
          int cbAttribute);
        ////Задает значение неклиентских атрибутов отображения Desktop Window Manager (DWM) для окна.
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(
          IntPtr hwnd,
          int attr,
          ref int attrValue,
          int attrSize);
        //Удаляет связь миниатюр диспетчера окон рабочего стола (DWM), созданную функцией DwmRegisterThumbnail.
        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr hThumbnailId);
        //Обновляет свойства эскиза диспетчера окон рабочего стола (DWM).
        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(
          IntPtr hThumbnailId,
          ref DWM_THUMBNAIL_PROPERTIES ptnProperties);
        //Включает эффект размытия в указанном окне.
        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableBlurBehindWindow(
          IntPtr hWnd,
          ref DWM_BLURBEHIND pBlurBehind);
        //Задает атрибуты для управления применением визуальных стилей к указанному окну.
        [DllImport("uxtheme.dll")]
        public static extern int SetWindowThemeAttribute(
          IntPtr hWnd,
          WindowThemeAttributeType wtype,
          ref WTA_OPTIONS attributes,
          uint size);

        public DwmApi() { }
        static DwmApi()
        {
            WTNCA_NODRAWCAPTION = 1U;
            WTNCA_NODRAWICON = 2U;
            WTNCA_NOSYSMENU = 4U;
            WTNCA_NOMIRRORHELP = 8U;
            DwmApiAvailable = Environment.OSVersion.Version.Major >= 6;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RECT
        {
            [FieldOffset(12)]
            public int bottom;
            [FieldOffset(0)]
            public int left;
            [FieldOffset(8)]
            public int right;
            [FieldOffset(4)]
            public int top;

            public RECT(Rectangle rect)
            {
                this.left = rect.Left;
                this.top = rect.Top;
                this.right = rect.Right;
                this.bottom = rect.Bottom;
            }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public void Set()
            {
                this.left = DwmApi.RECT.InlineAssignHelper<int>(ref this.top, DwmApi.RECT.InlineAssignHelper<int>(ref this.right, DwmApi.RECT.InlineAssignHelper<int>(ref this.bottom, 0)));
            }

            public void Set(Rectangle rect)
            {
                this.left = rect.Left;
                this.top = rect.Top;
                this.right = rect.Right;
                this.bottom = rect.Bottom;
            }

            public void Set(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public Rectangle ToRectangle()
            {
                return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
            }

            public int Height
            {
                get
                {
                    return this.bottom - this.top;
                }
            }

            public Size Size
            {
                get
                {
                    return new Size(this.Width, this.Height);
                }
            }

            public int Width
            {
                get
                {
                    return this.right - this.left;
                }
            }

            private static T InlineAssignHelper<T>(ref T target, T value)
            {
                target = value;
                return value;
            }
        }
        //Задает свойства размытия окна рабочего стола (DWM). Используется функцией DwmEnableBlurBehindWindow.
        public struct DWM_BLURBEHIND
        {
            public const int DWM_BB_ENABLE = 1;
            public const int DWM_BB_BLURREGION = 2;
            public const int DWM_BB_TRANSITIONONMAXIMIZED = 4;
            public int dwFlags;
            public int fEnable;
            public IntPtr hRgnBlur;
            public int fTransitionOnMaximized;
            public static DWM_BLURBEHIND Enable;
            public static DWM_BLURBEHIND Disable;

            private DWM_BLURBEHIND(bool enable)
            {
                this.dwFlags = 1;
                this.fEnable = enable ? 1 : 0;
                this.hRgnBlur = IntPtr.Zero;
                this.fTransitionOnMaximized = 0;
            }

            static DWM_BLURBEHIND()
            {
                DwmApi.DWM_BLURBEHIND.Enable = new DwmApi.DWM_BLURBEHIND(true);
                DwmApi.DWM_BLURBEHIND.Disable = new DwmApi.DWM_BLURBEHIND(false);
            }
        }
        //Задает параметры видеокадра Desktop Window Manager (DWM) для композиции кадра. Используется функцией DwmSetPresentParameters.
        public struct DWM_PRESENT_PARAMETERS
        {
            public int cbSize;
            public int fQueue;
            public long cRefreshStart;
            public int cBuffer;
            public int fUseSourceRate;
            public UNSIGNED_RATIO rateSource;
            public int cRefreshesPerFrame;
            public DWM_SOURCE_FRAME_SAMPLING eSampling;
        }
        //Задает свойства миниатюр диспетчера окон рабочего стола (DWM). Используется функцией DwmUpdateThumbnailProperties.
        public struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public RECT rcDestination;
            public RECT rcSource;
            public byte opacity;
            public int fVisible;
            public int fSourceClientAreaOnly;
        }
        //Задает сведения о времени композиции диспетчера окон рабочего стола (DWM). Используется функцией DwmGetCompositionTimingInfo.
        public struct DWM_TIMING_INFO
        {
            public int cbSize;
            public UNSIGNED_RATIO rateRefresh;
            public UNSIGNED_RATIO rateCompose;
            public long qpcVBlank;
            public long cRefresh;
            public long qpcCompose;
            public long cFrame;
            public long cRefreshFrame;
            public long cRefreshConfirmed;
            public int cFlipsOutstanding;
            public long cFrameCurrent;
            public long cFramesAvailable;
            public long cFrameCleared;
            public long cFramesReceived;
            public long cFramesDisplayed;
            public long cFramesDropped;
            public long cFramesMissed;
        }
        //Определяет тип данных, используемый API диспетчера окон рабочего стола (DWM). Он представляет собой общий коэффициент и используется для различных целей и единиц даже в рамках одного API.
        public struct UNSIGNED_RATIO
        {
            public int uiNumerator;
            public int uiDenominator;
        }
        //Возвращается функцией GetThemeMargins для определения полей окон, к которым применяются визуальные стили.
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;

            public MARGINS(int Left, int Right, int Top, int Bottom)
            {
                this.cxLeftWidth = Left;
                this.cxRightWidth = Right;
                this.cyTopHeight = Top;
                this.cyBottomHeight = Bottom;
            }
        }
        //Определяет параметры, используемые для установки атрибутов визуального стиля окна.
        public struct WTA_OPTIONS
        {
            public uint Flags;
            public uint Mask;
        }
        //Флаги, используемые функцией DwmSetPresentParameters для указания типа выборки кадров.
        public enum DWM_SOURCE_FRAME_SAMPLING
        {
            DWM_SOURCE_FRAME_SAMPLING_POINT,
            DWM_SOURCE_FRAME_SAMPLING_COVERAGE,
            DWM_SOURCE_FRAME_SAMPLING_LAST,
        }
        //Флаги, используемые функцией DwmSetWindowAttribute для указания политики отображения неклиентской области.
        public enum DWMNCRENDERINGPOLICY
        {
            DWMNCRP_USEWINDOWSTYLE,
            DWMNCRP_DISABLED,
            DWMNCRP_ENABLED,
        }
        //Флаги, используемые функциями DwmGetWindowAttribute и DwmSetWindowAttribute для указания атрибутов окна для неклиентской визуализации диспетчера окон рабочего стола (DWM).
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY = 2,
            DWMWA_TRANSITIONS_FORCEDISABLED = 3,
            DWMWA_ALLOW_NCPAINT = 4,
            DWMWA_CAPTION_BUTTON_BOUNDS = 5,
            DWMWA_NONCLIENT_RTL_LAYOUT = 6,
            DWMWA_FORCE_ICONIC_REPRESENTATION = 7,
            DWMWA_FLIP3D_POLICY = 8,
            DWMWA_LAST = 9,
        }
        //Задает тип атрибута визуального стиля для установки в окне.
        public enum WindowThemeAttributeType
        {
            WTA_NONCLIENT = 1,
        }
    }
    /// <summary>
    /// Обеспечивает работу с низкоуровневыми функциями Windows
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class WinApi
    {
        public const int Autohide = 1;
        public const int AlwaysOnTop = 2;
        public const int MfByposition = 1024;
        public const int MfRemove = 4096;
        public const int TCM_HITTEST = 4883;
        public const int ULW_COLORKEY = 1;
        public const int ULW_ALPHA = 2;
        public const int ULW_OPAQUE = 4;
        public const byte AC_SRC_OVER = 0;
        public const byte AC_SRC_ALPHA = 1;
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int HC_ACTION = 0;
        public const int WH_CALLWNDPROC = 4;
        public const int GWL_WNDPROC = -4;

        //Обновляет положение, размер, форму, содержимое и прозрачность многоуровневого окна.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(
          IntPtr hwnd,
          IntPtr hdcDst,
          ref POINT pptDst,
          ref SIZE psize,
          IntPtr hdcSrc,
          ref POINT pprSrc,
          int crKey,
          ref BLENDFUNCTION pblend,
          int dwFlags);
        //Функция GetDC извлекает дескриптор контекста устройства (DC) для клиентской области указанного окна или для всего экрана.
        //Контекст устройства-это непрозрачная структура данных,значения которой используются внутри GDI.
        //Функция GetDCEx является расширением для получения DC, что дает приложению больше контроля над тем, как и происходит ли отсечение в клиентской области.

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);
        //Создает контекст устройства памяти (DC), совместимый с указанным устройством.
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        //Удаляет указанный контекст устройства (DC).
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);
        //Выбирает объект в указанном контексте устройства (DC). Новый объект заменяет предыдущий объект того же типа.
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        //Удаляет логическое перо, кисть, шрифт, растровое изображение, область или палитру, освобождая все системные ресурсы, связанные с объектом. 
        //После удаления объекта указанный дескриптор становится недействительным.
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);
        //Извлекает информацию об указанном окне. Функция также извлекает 32-разрядное значение (DWORD) с указанным смещением в дополнительную память окна.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        //Изменяет атрибут указанного окна. Функция также устанавливает 32-разрядное (длинное) значение с заданным смещением в дополнительную память окна.
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        //Изменяет размер, положение и порядок Z дочернего, всплывающего или верхнего окна. Эти окна упорядочены в соответствии с их внешним видом на экране. 
        //Самое верхнее окно получает самый высокий ранг и является первым окном в порядке Z.
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(
          IntPtr hWnd,
          IntPtr hWndInsertAfter,
          int X,
          int Y,
          int W,
          int H,
          uint uFlags);
        //Позволяет приложению получить доступ к меню окна (также известному как системное меню или меню управления) для копирования и изменения.
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        //Определяет количество элементов в указанном меню.
        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);
        //Перерисовывает строку меню указанного окна. Если строка меню изменяется после того, как система создала окно, эта функция должна быть вызвана для отображения измененной строки меню.
        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);
        //Удаляет элемент меню или отсоединяет подменю от указанного меню. Если пункт меню открывает раскрывающееся меню или подменю, удалить меню не уничтожает меню или его дескриптор, позволяя использовать меню повторно. 
        //Перед вызовом этой функции функция GetSubMenu должна получить дескриптор в раскрывающемся меню или подменю.
        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        //Освобождает захват мыши из окна в текущем потоке и восстанавливает нормальную обработку ввода мыши. 
        //Окно, которое захватило мышь, получает все входные данные мыши, независимо от положения курсора, за исключением случаев, когда кнопка мыши нажата, когда горячая точка курсора находится в окне другого потока.
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        //Устанавливает захват мыши в указанное окно, принадлежащее текущему потоку.
        //SetCapture захватывает ввод мыши либо когда мышь находится над окном захвата, либо когда кнопка мыши была нажата, когда мышь была над окном захвата, и кнопка все еще не нажата. Только одно окно за один раз может захватить мышь.
        //Если курсор мыши находится над окном, созданным другим потоком, система будет направлять ввод мыши в указанное окно только при нажатой кнопке мыши.
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);
        //Отправляет указанное сообщение в окно или окна. Функция SendMessage вызывает процедуру window для указанного окна и не возвращается, пока процедура window не обработает сообщение.
        //Для отправки сообщения и немедленного возврата используйте функцию SendMessageCallback или SendNotifyMessage.Чтобы отправить сообщение в очередь сообщений потока и немедленно вернуться, используйте функцию PostMessage или PostThreadMessage.     
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //Отправляет указанное сообщение в окно или окна. Функция SendMessage вызывает процедуру window для указанного окна и не возвращается, пока процедура window не обработает сообщение.
        //Для отправки сообщения и немедленного возврата используйте функцию SendMessageCallback или SendNotifyMessage.Чтобы отправить сообщение в очередь сообщений потока и немедленно вернуться, используйте функцию PostMessage или PostThreadMessage.
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr wnd, int msg, bool param, int lparam);
        //Отправляет сообщение панели приложений в систему.
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr SHAppBarMessage(
          WinApi.ABM dwMessage,
          [In] ref APPBARDATA pData);
        //Возвращает дескриптор окна верхнего уровня, имя класса и имя окна которого соответствуют указанным строкам. Эта функция не выполняет поиск дочерних окон. Эта функция не выполняет поиск с учетом регистра.
        //Для поиска дочерних окон, начиная с указанного дочернего окна, используйте функцию FindWindowEx.        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //Выводит поток, создавший указанное окно, на передний план и активирует окно. Ввод с клавиатуры направляется в окно, и различные визуальные подсказки изменяются для пользователя. 
        //Система назначает несколько более высокий приоритет потоку, создавшему окно переднего плана, чем другим потокам.
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        //Функция GetDCEx извлекает дескриптор контекста устройства (DC) для клиентской области указанного окна или для всего экрана. Контекст устройства-это непрозрачная структура данных,значения которой используются внутри GDI.
        //Эта функция является расширением функции GetDC, которая предоставляет приложению больший контроль над тем, как и происходит ли отсечение в клиентской области.      
        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);
        //Показывает или скрывает указанную полосу прокрутки.
        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int bar, int cmd);
        //Функция GetWindowDC извлекает контекст устройства (DC) для всего окна, включая строку заголовка, меню и полосы прокрутки. 
        //Контекст оконного устройства позволяет рисовать в любом месте окна, поскольку источником контекста устройства является верхний левый угол окна, а не клиентская область.
        //GetWindowDC присваивает атрибуты по умолчанию контексту устройства окна каждый раз, когда он извлекает контекст устройства.Предыдущие атрибуты теряются.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);
        //Функция ReleaseDC освобождает контекст устройства (DC), освобождая его для использования другими приложениями. Эффект функции ReleaseDC зависит от типа постоянного тока. 
        //Он освобождает только общие и оконные контроллеры домена. Это не влияет на класс или частные контроллеры домена.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);
        //Возвращает имя класса, к которому принадлежит указанное окно.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);
        //Возвращает дескриптор окна, имеющего указанное отношение (Z-Order или owner) к указанному окну.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);
        //Определяет состояние видимости указанного окна.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);
        //Получает координаты клиентской области окна. Координаты клиента определяют верхний левый и нижний правый углы клиентской области. 
        //Поскольку координаты клиента относятся к верхнему левому углу клиентской области окна, координаты верхнего левого угла равны (0,0).
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);
        //Получает координаты клиентской области окна. Координаты клиента определяют верхний левый и нижний правый углы клиентской области. 
        //Поскольку координаты клиента относятся к верхнему левому углу клиентской области окна, координаты верхнего левого угла равны (0,0).
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect);
        //Изменяет положение и размеры указанного окна. Для окна верхнего уровня положение и размеры отображаются относительно левого верхнего угла экрана. 
        //Для дочернего окна они расположены относительно верхнего левого угла клиентской области родительского окна.
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(
          IntPtr hwnd,
          int X,
          int Y,
          int nWidth,
          int nHeight,
          bool bRepaint);
        //Функция UpdateWindow обновляет клиентскую область указанного окна, отправляя сообщение WM_PAINT в окно, если область обновления окна не пуста. 
        //Функция отправляет сообщение WM_PAINT непосредственно в оконную процедуру указанного окна, минуя очередь приложений. Если область обновления пуста, сообщение не отправляется.
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);
        //Функция InvalidateRect добавляет прямоугольник в область обновления указанного окна. Область обновления представляет собой часть клиентской области окна, которая должна быть перерисована.
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);
        //Функция ValidateRect проверяет клиентскую область в пределах прямоугольника, удаляя прямоугольник из области обновления указанного окна.
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);
        //Извлекает размеры ограничивающего прямоугольника указанного окна. Размеры задаются в координатах экрана относительно верхнего левого угла экрана.
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);
        public static int LoWord(int dwValue)
        {
            return dwValue & (int)ushort.MaxValue;
        }
        public static int HiWord(int dwValue)
        {
            return dwValue >> 16 & (int)ushort.MaxValue;
        }
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }
        //Структура BLENDFUNCTION управляет смешиванием, задавая функции смешивания для исходных и целевых растровых изображений.
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        //Содержит информацию о тесте попадания. Эта структура заменяет структуру TC_HITTESTINFO.
        public struct TCHITTESTINFO
        {
            public Point pt;
            public uint flags;
        }

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(Rectangle rc)
            {
                this.Left = rc.Left;
                this.Top = rc.Top;
                this.Right = rc.Right;
                this.Bottom = rc.Bottom;
            }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
            }
        }
        //Содержит информацию, которую приложение может использовать при обработке сообщения WM_NCCALCSIZE для вычисления размера, положения и допустимого содержимого клиентской области окна.
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rect0;
            public RECT rect1;
            public RECT rect2;
            public IntPtr lppos;
        }
        //Содержит информацию о максимальном размере и положении окна, а также его минимальном и максимальном размере отслеживания.
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }
        //Содержит информацию о системном сообщении панели приложений.
        public struct APPBARDATA
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public ABE uEdge;
            public RECT rc;
            public int lParam;
        }
        //Содержит информацию о размере и положении окна.
        public struct WindowPos
        {
            public int hwnd;
            public int hWndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }
        public enum ABM : uint
        {
            New,
            Remove,
            QueryPos,
            SetPos,
            GetState,
            GetTaskbarPos,
            Activate,
            GetAutoHideBar,
            SetAutoHideBar,
            WindowPosChanged,
            SetState,
        }
        public enum ABE : uint
        {
            Left,
            Top,
            Right,
            Bottom,
        }
        public enum ScrollBar
        {
            SB_HORZ,
            SB_VERT,
            SB_CTL,
            SB_BOTH,
        }
        public enum HitTest
        {
            HTTRANSPARENT = -1, // 0xFFFFFFFF
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTGROWBOX = 4,
            HTSIZE = 4,
            HTMINBUTTON = 8,
            HTREDUCE = 8,
            HTMAXBUTTON = 9,
            HTZOOM = 9,
            HTLEFT = 10, // 0x0000000A
            HTSIZEFIRST = 10, // 0x0000000A
            HTRIGHT = 11, // 0x0000000B
            HTTOP = 12, // 0x0000000C
            HTTOPLEFT = 13, // 0x0000000D
            HTTOPRIGHT = 14, // 0x0000000E
            HTBOTTOM = 15, // 0x0000000F
            HTBOTTOMLEFT = 16, // 0x00000010
            HTBOTTOMRIGHT = 17, // 0x00000011
            HTSIZELAST = 17, // 0x00000011
        }
        public enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
        }
        public enum Messages : uint
        {
            WM_NULL = 0,
            WM_CREATE = 1,
            WM_DESTROY = 2,
            WM_MOVE = 3,
            WM_SIZE = 5,
            WM_ACTIVATE = 6,
            WM_SETFOCUS = 7,
            WM_KILLFOCUS = 8,
            WM_ENABLE = 10, // 0x0000000A
            WM_SETREDRAW = 11, // 0x0000000B
            WM_SETTEXT = 12, // 0x0000000C
            WM_GETTEXT = 13, // 0x0000000D
            WM_GETTEXTLENGTH = 14, // 0x0000000E
            WM_PAINT = 15, // 0x0000000F
            WM_CLOSE = 16, // 0x00000010
            WM_QUERYENDSESSION = 17, // 0x00000011
            WM_QUIT = 18, // 0x00000012
            WM_QUERYOPEN = 19, // 0x00000013
            WM_ERASEBKGND = 20, // 0x00000014
            WM_SYSCOLORCHANGE = 21, // 0x00000015
            WM_ENDSESSION = 22, // 0x00000016
            WM_SHOWWINDOW = 24, // 0x00000018
            WM_CTLCOLOR = 25, // 0x00000019
            WM_SETTINGCHANGE = 26, // 0x0000001A
            WM_WININICHANGE = 26, // 0x0000001A
            WM_DEVMODECHANGE = 27, // 0x0000001B
            WM_ACTIVATEAPP = 28, // 0x0000001C
            WM_FONTCHANGE = 29, // 0x0000001D
            WM_TIMECHANGE = 30, // 0x0000001E
            WM_CANCELMODE = 31, // 0x0000001F
            WM_SETCURSOR = 32, // 0x00000020
            WM_MOUSEACTIVATE = 33, // 0x00000021
            WM_CHILDACTIVATE = 34, // 0x00000022
            WM_QUEUESYNC = 35, // 0x00000023
            WM_GETMINMAXINFO = 36, // 0x00000024
            WM_PAINTICON = 38, // 0x00000026
            WM_ICONERASEBKGND = 39, // 0x00000027
            WM_NEXTDLGCTL = 40, // 0x00000028
            WM_SPOOLERSTATUS = 42, // 0x0000002A
            WM_DRAWITEM = 43, // 0x0000002B
            WM_MEASUREITEM = 44, // 0x0000002C
            WM_DELETEITEM = 45, // 0x0000002D
            WM_VKEYTOITEM = 46, // 0x0000002E
            WM_CHARTOITEM = 47, // 0x0000002F
            WM_SETFONT = 48, // 0x00000030
            WM_GETFONT = 49, // 0x00000031
            WM_SETHOTKEY = 50, // 0x00000032
            WM_GETHOTKEY = 51, // 0x00000033
            WM_QUERYDRAGICON = 55, // 0x00000037
            WM_COMPAREITEM = 57, // 0x00000039
            WM_GETOBJECT = 61, // 0x0000003D
            WM_COMPACTING = 65, // 0x00000041
            WM_COMMNOTIFY = 68, // 0x00000044
            WM_WINDOWPOSCHANGING = 70, // 0x00000046
            WM_WINDOWPOSCHANGED = 71, // 0x00000047
            WM_POWER = 72, // 0x00000048
            WM_COPYDATA = 74, // 0x0000004A
            WM_CANCELJOURNAL = 75, // 0x0000004B
            WM_NOTIFY = 78, // 0x0000004E
            WM_INPUTLANGCHANGEREQUEST = 80, // 0x00000050
            WM_INPUTLANGCHANGE = 81, // 0x00000051
            WM_TCARD = 82, // 0x00000052
            WM_HELP = 83, // 0x00000053
            WM_USERCHANGED = 84, // 0x00000054
            WM_NOTIFYFORMAT = 85, // 0x00000055
            WM_CONTEXTMENU = 123, // 0x0000007B
            WM_STYLECHANGING = 124, // 0x0000007C
            WM_STYLECHANGED = 125, // 0x0000007D
            WM_DISPLAYCHANGE = 126, // 0x0000007E
            WM_GETICON = 127, // 0x0000007F
            WM_SETICON = 128, // 0x00000080
            WM_NCCREATE = 129, // 0x00000081
            WM_NCDESTROY = 130, // 0x00000082
            WM_NCCALCSIZE = 131, // 0x00000083
            WM_NCHITTEST = 132, // 0x00000084
            WM_NCPAINT = 133, // 0x00000085
            WM_NCACTIVATE = 134, // 0x00000086
            WM_GETDLGCODE = 135, // 0x00000087
            WM_SYNCPAINT = 136, // 0x00000088
            WM_NCMOUSEMOVE = 160, // 0x000000A0
            WM_NCLBUTTONDOWN = 161, // 0x000000A1
            WM_NCLBUTTONUP = 162, // 0x000000A2
            WM_NCLBUTTONDBLCLK = 163, // 0x000000A3
            WM_NCRBUTTONDOWN = 164, // 0x000000A4
            WM_NCRBUTTONUP = 165, // 0x000000A5
            WM_NCRBUTTONDBLCLK = 166, // 0x000000A6
            WM_NCMBUTTONDOWN = 167, // 0x000000A7
            WM_NCMBUTTONUP = 168, // 0x000000A8
            WM_NCMBUTTONDBLCLK = 169, // 0x000000A9
            WM_NCXBUTTONDOWN = 171, // 0x000000AB
            WM_NCXBUTTONUP = 172, // 0x000000AC
            WM_NCXBUTTONDBLCLK = 173, // 0x000000AD
            WM_INPUT = 255, // 0x000000FF
            WM_KEYDOWN = 256, // 0x00000100
            WM_KEYFIRST = 256, // 0x00000100
            WM_KEYUP = 257, // 0x00000101
            WM_CHAR = 258, // 0x00000102
            WM_DEADCHAR = 259, // 0x00000103
            WM_SYSKEYDOWN = 260, // 0x00000104
            WM_SYSKEYUP = 261, // 0x00000105
            WM_SYSCHAR = 262, // 0x00000106
            WM_SYSDEADCHAR = 263, // 0x00000107
            WM_KEYLAST = 264, // 0x00000108
            WM_UNICHAR = 265, // 0x00000109
            WM_IME_STARTCOMPOSITION = 269, // 0x0000010D
            WM_IME_ENDCOMPOSITION = 270, // 0x0000010E
            WM_IME_COMPOSITION = 271, // 0x0000010F
            WM_IME_KEYLAST = 271, // 0x0000010F
            WM_INITDIALOG = 272, // 0x00000110
            WM_COMMAND = 273, // 0x00000111
            WM_SYSCOMMAND = 274, // 0x00000112
            WM_TIMER = 275, // 0x00000113
            WM_HSCROLL = 276, // 0x00000114
            WM_VSCROLL = 277, // 0x00000115
            WM_INITMENU = 278, // 0x00000116
            WM_INITMENUPOPUP = 279, // 0x00000117
            WM_MENUSELECT = 287, // 0x0000011F
            WM_MENUCHAR = 288, // 0x00000120
            WM_ENTERIDLE = 289, // 0x00000121
            WM_MENURBUTTONUP = 290, // 0x00000122
            WM_MENUDRAG = 291, // 0x00000123
            WM_MENUGETOBJECT = 292, // 0x00000124
            WM_UNINITMENUPOPUP = 293, // 0x00000125
            WM_MENUCOMMAND = 294, // 0x00000126
            WM_CHANGEUISTATE = 295, // 0x00000127
            WM_UPDATEUISTATE = 296, // 0x00000128
            WM_QUERYUISTATE = 297, // 0x00000129
            WM_CTLCOLORMSGBOX = 306, // 0x00000132
            WM_CTLCOLOREDIT = 307, // 0x00000133
            WM_CTLCOLORLISTBOX = 308, // 0x00000134
            WM_CTLCOLORBTN = 309, // 0x00000135
            WM_CTLCOLORDLG = 310, // 0x00000136
            WM_CTLCOLORSCROLLBAR = 311, // 0x00000137
            WM_CTLCOLORSTATIC = 312, // 0x00000138
            WM_MOUSEFIRST = 512, // 0x00000200
            WM_MOUSEMOVE = 512, // 0x00000200
            WM_LBUTTONDOWN = 513, // 0x00000201
            WM_LBUTTONUP = 514, // 0x00000202
            WM_LBUTTONDBLCLK = 515, // 0x00000203
            WM_RBUTTONDOWN = 516, // 0x00000204
            WM_RBUTTONUP = 517, // 0x00000205
            WM_RBUTTONDBLCLK = 518, // 0x00000206
            WM_MBUTTONDOWN = 519, // 0x00000207
            WM_MBUTTONUP = 520, // 0x00000208
            WM_MBUTTONDBLCLK = 521, // 0x00000209
            WM_MOUSEWHEEL = 522, // 0x0000020A
            WM_XBUTTONDOWN = 523, // 0x0000020B
            WM_XBUTTONUP = 524, // 0x0000020C
            WM_MOUSELAST = 525, // 0x0000020D
            WM_XBUTTONDBLCLK = 525, // 0x0000020D
            WM_PARENTNOTIFY = 528, // 0x00000210
            WM_ENTERMENULOOP = 529, // 0x00000211
            WM_EXITMENULOOP = 530, // 0x00000212
            WM_NEXTMENU = 531, // 0x00000213
            WM_SIZING = 532, // 0x00000214
            WM_CAPTURECHANGED = 533, // 0x00000215
            WM_MOVING = 534, // 0x00000216
            WM_POWERBROADCAST = 536, // 0x00000218
            WM_DEVICECHANGE = 537, // 0x00000219
            WM_MDICREATE = 544, // 0x00000220
            WM_MDIDESTROY = 545, // 0x00000221
            WM_MDIACTIVATE = 546, // 0x00000222
            WM_MDIRESTORE = 547, // 0x00000223
            WM_MDINEXT = 548, // 0x00000224
            WM_MDIMAXIMIZE = 549, // 0x00000225
            WM_MDITILE = 550, // 0x00000226
            WM_MDICASCADE = 551, // 0x00000227
            WM_MDIICONARRANGE = 552, // 0x00000228
            WM_MDIGETACTIVE = 553, // 0x00000229
            WM_MDISETMENU = 560, // 0x00000230
            WM_ENTERSIZEMOVE = 561, // 0x00000231
            WM_EXITSIZEMOVE = 562, // 0x00000232
            WM_DROPFILES = 563, // 0x00000233
            WM_MDIREFRESHMENU = 564, // 0x00000234
            WM_IME_SETCONTEXT = 641, // 0x00000281
            WM_IME_NOTIFY = 642, // 0x00000282
            WM_IME_CONTROL = 643, // 0x00000283
            WM_IME_COMPOSITIONFULL = 644, // 0x00000284
            WM_IME_SELECT = 645, // 0x00000285
            WM_IME_CHAR = 646, // 0x00000286
            WM_IME_REQUEST = 648, // 0x00000288
            WM_IME_KEYDOWN = 656, // 0x00000290
            WM_IME_KEYUP = 657, // 0x00000291
            WM_MOUSEHOVER = 673, // 0x000002A1
            WM_NCMOUSELEAVE = 674, // 0x000002A2
            WM_MOUSELEAVE = 675, // 0x000002A3
            WM_WTSSESSION_CHANGE = 689, // 0x000002B1
            WM_TABLET_FIRST = 704, // 0x000002C0
            WM_TABLET_LAST = 735, // 0x000002DF
            WM_CUT = 768, // 0x00000300
            WM_COPY = 769, // 0x00000301
            WM_PASTE = 770, // 0x00000302
            WM_CLEAR = 771, // 0x00000303
            WM_UNDO = 772, // 0x00000304
            WM_RENDERFORMAT = 773, // 0x00000305
            WM_RENDERALLFORMATS = 774, // 0x00000306
            WM_DESTROYCLIPBOARD = 775, // 0x00000307
            WM_DRAWCLIPBOARD = 776, // 0x00000308
            WM_PAINTCLIPBOARD = 777, // 0x00000309
            WM_VSCROLLCLIPBOARD = 778, // 0x0000030A
            WM_SIZECLIPBOARD = 779, // 0x0000030B
            WM_ASKCBFORMATNAME = 780, // 0x0000030C
            WM_CHANGECBCHAIN = 781, // 0x0000030D
            WM_HSCROLLCLIPBOARD = 782, // 0x0000030E
            WM_QUERYNEWPALETTE = 783, // 0x0000030F
            WM_PALETTEISCHANGING = 784, // 0x00000310
            WM_PALETTECHANGED = 785, // 0x00000311
            WM_HOTKEY = 786, // 0x00000312
            WM_PRINT = 791, // 0x00000317
            WM_PRINTCLIENT = 792, // 0x00000318
            WM_APPCOMMAND = 793, // 0x00000319
            WM_THEMECHANGED = 794, // 0x0000031A
            WM_DWMCOMPOSITIONCHANGED = 798, // 0x0000031E
            WM_HANDHELDFIRST = 856, // 0x00000358
            WM_HANDHELDLAST = 863, // 0x0000035F
            WM_AFXFIRST = 864, // 0x00000360
            WM_AFXLAST = 895, // 0x0000037F
            WM_PENWINFIRST = 896, // 0x00000380
            WM_PENWINLAST = 911, // 0x0000038F
            WM_USER = 1024, // 0x00000400
            WM_REFLECT = 8192, // 0x00002000
            WM_APP = 32768, // 0x00008000
            SC_MOVE = 61456, // 0x0000F010
            SC_MINIMIZE = 61472, // 0x0000F020
            SC_MAXIMIZE = 61488, // 0x0000F030
            SC_RESTORE = 61728, // 0x0000F120
        }
        public enum Bool
        {
            False,
            True,
        }
    }
    /// <summary>
    /// Обеспечивает работу с низкоуровневыми функциями Windows
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class Win32Api
    {
        internal const int KEY_QUERY_VALUE = 1;
        internal const int KEY_SET_VALUE = 2;
        internal const int KEY_CREATE_SUB_KEY = 4;
        internal const int KEY_ENUMERATE_SUB_KEYS = 8;
        internal const int KEY_NOTIFY = 16;
        internal const int KEY_CREATE_LINK = 32;
        internal const int KEY_READ = 131097;
        internal const int KEY_WRITE = 131078;
        internal const int REG_NONE = 0;
        internal const int REG_SZ = 1;
        internal const int REG_EXPAND_SZ = 2;
        internal const int REG_BINARY = 3;
        internal const int REG_DWORD = 4;
        internal const int REG_DWORD_LITTLE_ENDIAN = 4;
        internal const int REG_DWORD_BIG_ENDIAN = 5;
        internal const int REG_LINK = 6;
        internal const int REG_MULTI_SZ = 7;
        internal const int REG_RESOURCE_LIST = 8;
        internal const int REG_FULL_RESOURCE_DESCRIPTOR = 9;
        internal const int REG_RESOURCE_REQUIREMENTS_LIST = 10;
        internal const int REG_QWORD = 11;
        internal const int HWND_BROADCAST = 65535;
        internal const int WM_SETTINGCHANGE = 26;
        internal const uint CRYPTPROTECTMEMORY_BLOCK_SIZE = 16;
        internal const uint CRYPTPROTECTMEMORY_SAME_PROCESS = 0;
        internal const uint CRYPTPROTECTMEMORY_CROSS_PROCESS = 1;
        internal const uint CRYPTPROTECTMEMORY_SAME_LOGON = 2;
        internal const int SECURITY_ANONYMOUS = 0;
        internal const int SECURITY_SQOS_PRESENT = 1048576;
        internal const string MICROSOFT_KERBEROS_NAME = "Kerberos";
        internal const uint ANONYMOUS_LOGON_LUID = 998;
        internal const int SECURITY_ANONYMOUS_LOGON_RID = 7;
        internal const int SECURITY_AUTHENTICATED_USER_RID = 11;
        internal const int SECURITY_LOCAL_SYSTEM_RID = 18;
        internal const int SECURITY_BUILTIN_DOMAIN_RID = 32;
        internal const int DOMAIN_USER_RID_GUEST = 501;
        internal const uint SE_PRIVILEGE_DISABLED = 0;
        internal const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1;
        internal const uint SE_PRIVILEGE_ENABLED = 2;
        internal const uint SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648;
        internal const uint SE_GROUP_MANDATORY = 1;
        internal const uint SE_GROUP_ENABLED_BY_DEFAULT = 2;
        internal const uint SE_GROUP_ENABLED = 4;
        internal const uint SE_GROUP_OWNER = 8;
        internal const uint SE_GROUP_USE_FOR_DENY_ONLY = 16;
        internal const uint SE_GROUP_LOGON_ID = 3221225472;
        internal const uint SE_GROUP_RESOURCE = 536870912;
        internal const uint DUPLICATE_CLOSE_SOURCE = 1;
        internal const uint DUPLICATE_SAME_ACCESS = 2;
        internal const uint DUPLICATE_SAME_ATTRIBUTES = 4;
        internal const int READ_CONTROL = 131072;
        internal const int SYNCHRONIZE = 1048576;
        internal const int STANDARD_RIGHTS_READ = 131072;
        internal const int STANDARD_RIGHTS_WRITE = 131072;
        internal const int SEMAPHORE_MODIFY_STATE = 2;
        internal const int EVENT_MODIFY_STATE = 2;
        internal const int MUTEX_MODIFY_STATE = 1;
        internal const int MUTEX_ALL_ACCESS = 2031617;
        internal const int LMEM_FIXED = 0;
        internal const int LMEM_ZEROINIT = 64;
        internal const int LPTR = 64;
        internal const string LSTRCPY = "lstrcpy";
        internal const string LSTRCPYN = "lstrcpyn";
        internal const string LSTRLEN = "lstrlen";
        internal const string LSTRLENA = "lstrlenA";
        internal const string LSTRLENW = "lstrlenW";
        internal const string MOVEMEMORY = "RtlMoveMemory";
        internal const int SEM_FAILCRITICALERRORS = 1;
        internal const int LCMAP_SORTKEY = 1024;
        internal const int FIND_STARTSWITH = 1048576;
        internal const int FIND_ENDSWITH = 2097152;
        internal const int FIND_FROMSTART = 4194304;
        internal const int FIND_FROMEND = 8388608;
        internal const int STD_INPUT_HANDLE = -10;
        internal const int STD_OUTPUT_HANDLE = -11;
        internal const int STD_ERROR_HANDLE = -12;
        internal const int CTRL_C_EVENT = 0;
        internal const int CTRL_BREAK_EVENT = 1;
        internal const int CTRL_CLOSE_EVENT = 2;
        internal const int CTRL_LOGOFF_EVENT = 5;
        internal const int CTRL_SHUTDOWN_EVENT = 6;
        internal const short KEY_EVENT = 1;
        internal const int FILE_TYPE_DISK = 1;
        internal const int FILE_TYPE_CHAR = 2;
        internal const int FILE_TYPE_PIPE = 3;
        internal const int REPLACEFILE_WRITE_THROUGH = 1;
        internal const int REPLACEFILE_IGNORE_MERGE_ERRORS = 2;
        internal const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
        internal const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
        internal const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
        internal const int FILE_ATTRIBUTE_READONLY = 1;
        internal const int FILE_ATTRIBUTE_DIRECTORY = 16;
        internal const int FILE_ATTRIBUTE_REPARSE_POINT = 1024;
        internal const int IO_REPARSE_TAG_MOUNT_POINT = -1610612733;
        internal const int PAGE_READWRITE = 4;
        internal const int MEM_COMMIT = 4096;
        internal const int MEM_RESERVE = 8192;
        internal const int MEM_RELEASE = 32768;
        internal const int MEM_FREE = 65536;
        internal const int ERROR_SUCCESS = 0;
        internal const int ERROR_INVALID_FUNCTION = 1;
        internal const int ERROR_FILE_NOT_FOUND = 2;
        internal const int ERROR_PATH_NOT_FOUND = 3;
        internal const int ERROR_ACCESS_DENIED = 5;
        internal const int ERROR_INVALID_HANDLE = 6;
        internal const int ERROR_NOT_ENOUGH_MEMORY = 8;
        internal const int ERROR_INVALID_DATA = 13;
        internal const int ERROR_INVALID_DRIVE = 15;
        internal const int ERROR_NO_MORE_FILES = 18;
        internal const int ERROR_NOT_READY = 21;
        internal const int ERROR_BAD_LENGTH = 24;
        internal const int ERROR_SHARING_VIOLATION = 32;
        internal const int ERROR_NOT_SUPPORTED = 50;
        internal const int ERROR_FILE_EXISTS = 80;
        internal const int ERROR_INVALID_PARAMETER = 87;
        internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;
        internal const int ERROR_INSUFFICIENT_BUFFER = 122;
        internal const int ERROR_INVALID_NAME = 123;
        internal const int ERROR_BAD_PATHNAME = 161;
        internal const int ERROR_ALREADY_EXISTS = 183;
        internal const int ERROR_ENVVAR_NOT_FOUND = 203;
        internal const int ERROR_FILENAME_EXCED_RANGE = 206;
        internal const int ERROR_NO_DATA = 232;
        internal const int ERROR_PIPE_NOT_CONNECTED = 233;
        internal const int ERROR_MORE_DATA = 234;
        internal const int ERROR_OPERATION_ABORTED = 995;
        internal const int ERROR_NO_TOKEN = 1008;
        internal const int ERROR_DLL_INIT_FAILED = 1114;
        internal const int ERROR_NON_ACCOUNT_SID = 1257;
        internal const int ERROR_NOT_ALL_ASSIGNED = 1300;
        internal const int ERROR_UNKNOWN_REVISION = 1305;
        internal const int ERROR_INVALID_OWNER = 1307;
        internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;
        internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;
        internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;
        internal const int ERROR_NONE_MAPPED = 1332;
        internal const int ERROR_INVALID_ACL = 1336;
        internal const int ERROR_INVALID_SID = 1337;
        internal const int ERROR_INVALID_SECURITY_DESCR = 1338;
        internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;
        internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;
        internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;
        internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;
        internal const uint STATUS_SUCCESS = 0;
        internal const uint STATUS_SOME_NOT_MAPPED = 263;
        internal const uint STATUS_NO_MEMORY = 3221225495;
        internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524;
        internal const uint STATUS_NONE_MAPPED = 3221225587;
        internal const uint STATUS_INSUFFICIENT_RESOURCES = 3221225626;
        internal const uint STATUS_ACCESS_DENIED = 3221225506;
        internal const int INVALID_FILE_SIZE = -1;
        internal const int STATUS_ACCOUNT_RESTRICTION = -1073741714;
        internal const int LCID_SUPPORTED = 2;
        internal const int ENABLE_PROCESSED_INPUT = 1;
        internal const int ENABLE_LINE_INPUT = 2;
        internal const int ENABLE_ECHO_INPUT = 4;
        internal const int VER_PLATFORM_WIN32s = 0;
        internal const int VER_PLATFORM_WIN32_WINDOWS = 1;
        internal const int VER_PLATFORM_WIN32_NT = 2;
        internal const int VER_PLATFORM_WINCE = 3;
        internal const int SHGFP_TYPE_CURRENT = 0;
        internal const int UOI_FLAGS = 1;
        internal const int WSF_VISIBLE = 1;
        internal const int CSIDL_APPDATA = 26;
        internal const int CSIDL_COMMON_APPDATA = 35;
        internal const int CSIDL_LOCAL_APPDATA = 28;
        internal const int CSIDL_COOKIES = 33;
        internal const int CSIDL_FAVORITES = 6;
        internal const int CSIDL_HISTORY = 34;
        internal const int CSIDL_INTERNET_CACHE = 32;
        internal const int CSIDL_PROGRAMS = 2;
        internal const int CSIDL_RECENT = 8;
        internal const int CSIDL_SENDTO = 9;
        internal const int CSIDL_STARTMENU = 11;
        internal const int CSIDL_STARTUP = 7;
        internal const int CSIDL_SYSTEM = 37;
        internal const int CSIDL_TEMPLATES = 21;
        internal const int CSIDL_DESKTOPDIRECTORY = 16;
        internal const int CSIDL_PERSONAL = 5;
        internal const int CSIDL_PROGRAM_FILES = 38;
        internal const int CSIDL_PROGRAM_FILES_COMMON = 43;
        internal const int CSIDL_DESKTOP = 0;
        internal const int CSIDL_DRIVES = 17;
        internal const int CSIDL_MYMUSIC = 13;
        internal const int CSIDL_MYPICTURES = 39;
        internal const int NameSamCompatible = 2;
        internal static readonly IntPtr INVALID_HANDLE_VALUE;
        internal static readonly IntPtr NULL;
        //Задает код последней ошибки для вызывающего потока.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void SetLastError(int errorCode);
        //[GetVersionEx может быть изменен или недоступен для выпусков после Windows 8.1. Вместо этого используйте вспомогательные функции версии]
        //С выпуском Windows 8.1 поведение API GetVersionEx изменилось в значении, которое он возвращает для версии операционной системы.Значение, возвращаемое функцией GetVersionEx, теперь зависит от того, как проявляется приложение.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool GetVersionEx([In, Out] OSVERSIONINFO ver);
        //Чтобы получить точную информацию для приложения, работающего на WOW64, вызовите функцию GetNativeSystemInfo.
        //Эта функция не возвращает значение.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);
        //Форматирует строку сообщения. Функция требует определения сообщения в качестве входных данных. Определение сообщения может поступать из буфера, переданного в функцию. 
        //Он может поступать из ресурса таблицы сообщений в уже загруженном модуле. Или вызывающий объект может попросить функцию выполнить поиск в системных ресурсах таблицы сообщений для определения сообщения. 
        //Функция находит определение сообщения в ресурсе таблицы сообщений на основе идентификатора сообщения и идентификатора языка. Функция копирует форматированный текст сообщения в выходной буфер, обрабатывая любые встроенные последовательности вставки, если требуется.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern int FormatMessage(
          int dwFlags,
          IntPtr lpSource,
          int dwMessageId,
          int dwLanguageId,
          StringBuilder lpBuffer,
          int nSize,
          IntPtr va_list_arguments);
        //Выделяет указанное количество байт из кучи.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("kernel32.dll", EntryPoint = "LocalAlloc")]
        internal static extern IntPtr LocalAlloc_NoSafeHandle(int uFlags, IntPtr sizetdwBytes);
        //Освобождает указанный объект локальной памяти и делает недействительным его дескриптор.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr LocalFree(IntPtr handle);
        //Заполняет блок памяти нулями
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void ZeroMemory(IntPtr handle, uint length);
        //Извлекает информацию о текущем использовании системой физической и виртуальной памяти.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX buffer);
        //Извлекает информацию о текущем использовании системой физической и виртуальной памяти.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GlobalMemoryStatus([In, Out] MEMORYSTATUS buffer);
        //Извлекает информацию о диапазоне страниц в виртуальном адресном пространстве вызывающего процесса.
        //Чтобы получить информацию о диапазоне страниц в адресном пространстве другого процесса, используйте функцию VirtualQueryEx.    
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe IntPtr VirtualQuery(
          void* address,
          ref MEMORY_BASIC_INFORMATION buffer,
          IntPtr sizeOfBuffer);
        //Резервирует, фиксирует или изменяет состояние области страниц в виртуальном адресном пространстве вызывающего процесса. Память, выделенная этой функцией, автоматически инициализируется до нуля.
        //Чтобы выделить память в адресном пространстве другого процесса, используйте функцию VirtualAllocEx.     
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe void* VirtualAlloc(
          void* address,
          UIntPtr numBytes,
          int commitOrReserve,
          int pageProtectionMode);
        //Освобождает, удаляет или освобождает и удаляет область страниц в виртуальном адресном пространстве вызывающего процесса.
        //Чтобы освободить память, выделенную в другом процессе функцией VirtualAllocEx, используйте функцию VirtualFreeEx.    
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe bool VirtualFree(
          void* address,
          UIntPtr numBytes,
          int pageFreeMode);
        //Извлекает путь к каталогу, предназначенному для временных файлов.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern uint GetTempPath(int bufferLen, StringBuilder buffer);
        //Выделяет новую строку, копирует указанное количество символов из переданной строки и добавляет завершающий символ null.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SysAllocStringLen(string src, int len);
        //Возвращает длину BSTR.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("oleaut32.dll")]
        internal static extern int SysStringLen(IntPtr bstr);
        //Освобождает строку, выделенную ранее SysAllocString, SysAllocStringByteLen, SysReAllocString, SysAllocStringLen или SysReAllocStringLen.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("oleaut32.dll")]
        internal static extern void SysFreeString(IntPtr bstr);
        //
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Unicode)]
        internal static extern void CopyMemoryUni(IntPtr pdst, string psrc, IntPtr sizetcb);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Unicode)]
        internal static extern void CopyMemoryUni(StringBuilder pdst, IntPtr psrc, IntPtr sizetcb);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern void CopyMemoryAnsi(IntPtr pdst, string psrc, IntPtr sizetcb);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern void CopyMemoryAnsi(StringBuilder pdst, IntPtr psrc, IntPtr sizetcb);
        //Извлекает текущий идентификатор кодовой страницы Windows ANSI для операционной системы.
        [DllImport("kernel32.dll")]
        internal static extern int GetACP();
        //Устанавливает указанный объект события в сигнальное состояние.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetEvent(SafeWaitHandle handle);
        //Устанавливает указанный объект события в состояние без сигнала.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ResetEvent(SafeWaitHandle handle);
        //Ожидает, пока один или все указанные объекты не перейдут в сигнальное состояние или не истечет интервал ожидания.
        //Чтобы ввести состояние ожидания оповещения, используйте функцию WaitForMultipleObjectsEx.    
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint WaitForMultipleObjects(
          uint nCount,
          IntPtr[] handles,
          bool bWaitAll,
          uint dwMilliseconds);
        //Создает или открывает именованный или неназванный объект события.
        //Чтобы задать маску доступа для объекта, используйте функцию CreateEventEx.      
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern SafeWaitHandle CreateEvent(
          SECURITY_ATTRIBUTES lpSecurityAttributes,
          bool isManualReset,
          bool initialState,
          string name);
        //Открывает существующий именованный объект события.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern SafeWaitHandle OpenEvent(
          int desiredAccess,
          bool inheritHandle,
          string name);
        //Создает или открывает именованный или неназванный объект мьютекса.
        //Чтобы задать маску доступа для объекта, используйте функцию Createmutexexw.       
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern SafeWaitHandle CreateMutex(
          SECURITY_ATTRIBUTES lpSecurityAttributes,
          bool initialOwner,
          string name);
        //Открывает существующий именованный объект мьютекса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern SafeWaitHandle OpenMutex(
          int desiredAccess,
          bool inheritHandle,
          string name);
        //Освобождает право собственности на указанный объект мьютекса.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReleaseMutex(SafeWaitHandle handle);

        //Закрывает дескриптор открытого объекта.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);

        //Извлекает сведения об объеме свободного пространства на диске, который является общим объемом пространства, общим объемом свободного пространства и общим объемом свободного пространства, доступного пользователю, связанному с вызывающим потоком.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool GetDiskFreeSpaceEx(
          string drive,
          out long freeBytesForUser,
          out long totalBytes,
          out long freeBytes);
        //Определяет, является ли диск съемным, фиксированным, CD-ROM, RAM-диском или сетевым диском.
        //Чтобы определить, является ли диск накопителем USB-типа, вызовите SetupDiGetDeviceRegistryProperty и укажите свойство SPDRP_REMOVAL_POLICY.       
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetDriveType(string drive);
        //Извлекает информацию о файловой системе и Томе, связанных с указанным корневым каталогом.
        //Чтобы указать дескриптор при получении этой информации, используйте функцию GetVolumeInformationByHandleW.
        //Чтобы получить текущее состояние сжатия файла или каталога, используйте FSCTL_GET_COMPRESSION.       
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool GetVolumeInformation(
          string drive,
          StringBuilder volumeName,
          int volumeNameBufLen,
          out int volSerialNumber,
          out int maxFileNameLen,
          out int fileSystemFlags,
          StringBuilder fileSystemName,
          int fileSystemNameBufLen);
        //Задает метку тома файловой системы.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool SetVolumeLabel(string driveLetter, string volumeName);
        //Извлекает путь к каталогу Windows.
        //Эта функция предоставляется в первую очередь для обеспечения совместимости с устаревшими приложениями.Новые приложения должны хранить код в папке Program Files и постоянные данные в папке Application Data в профиле пользователя.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetWindowsDirectory(StringBuilder sb, int length);
        //Для локали, заданной идентификатором, сопоставляет одну входную символьную строку с другой с помощью указанного преобразования или создает ключ сортировки для входной строки.
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe int LCMapStringW(
          int lcid,
          int flags,
          char* src,
          int cchSrc,
          char* target,
          int cchTarget);
        //Находит строку Юникода (широкие символы)или ее эквивалент в другой строке Юникода для локали, указанной идентификатором.
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe int FindNLSString(
          int Locale,
          int dwFindFlags,
          char* lpStringSource,
          int cchSource,
          char* lpStringValue,
          int cchValue,
          IntPtr pcchFound);

        //Извлекает дескриптор для указанного стандартного устройства (стандартный вход, стандартный выход или стандартная ошибка).
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int nStdHandle);

        internal static int MakeHRFromErrorCode(int errorCode)
        {
            return -2147024896 | errorCode;
        }

        //Извлекает битовую маску, представляющую доступные в данный момент диски.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int GetLogicalDrives();

        //Удаляет букву диска или смонтированную папку.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool DeleteVolumeMountPoint(string mountPoint);

        //Определяет, будет ли система обрабатывать указанные типы серьезных ошибок или процесс будет обрабатывать их.
        [DllImport("kernel32.dll")]
        internal static extern int SetErrorMode(int newMode);
        //Сопоставляет строку UTF-16 (широкий символ) с новой символьной строкой. Новая символьная строка не обязательно является многобайтовым набором символов.
        [DllImport("kernel32.dll")]
        internal static extern unsafe int WideCharToMultiByte(
          uint cp,
          uint flags,
          char* pwzSource,
          int cchSource,
          byte* pbDestBuffer,
          int cbDestBuffer,
          IntPtr null1,
          IntPtr null2);
        //Добавляет или удаляет определяемую приложением функцию подпрограммы обработчика из списка функций обработчика для вызывающего процесса.
        //Если функция обработчика не указана, она задает наследуемый атрибут, определяющий, игнорирует ли вызывающий процесс сигналы CTRL+C.       
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleCtrlHandler(
          ConsoleCtrlHandlerRoutine handler,
          bool addOrRemove);
        //Задает содержимое указанной переменной среды для текущего процесса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool SetEnvironmentVariable(string lpName, string lpValue);
        //Извлекает содержимое указанной переменной из блока среды вызывающего процесса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetEnvironmentVariable(
          string lpName,
          StringBuilder lpValue,
          int size);
        //Извлекает идентификатор вызывающего процесса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint GetCurrentProcessId();
        //Получает имя пользователя, связанного с текущим потоком.
        //Используйте функцию GetUserNameEx для получения имени пользователя в указанном формате.Дополнительная информация предоставляется интерфейсом IADsADSystemInfo.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetUserName(StringBuilder lpBuffer, ref int nSize);
        //Извлекает имя NetBIOS локального компьютера. Это имя устанавливается при запуске системы, когда система считывает его из реестра.
        //GetComputerName извлекает только имя NetBIOS локального компьютера.Чтобы получить имя узла DNS, доменное имя DNS или полное имя DNS, вызовите функцию GetComputerNameEx.Дополнительная информация предоставляется интерфейсом IADsADSystemInfo.
        //На поведение этой функции можно повлиять, если локальный компьютер является узлом в кластере.  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int GetComputerName(StringBuilder nameBuffer, ref int bufferSize);
        //Выделяет блок памяти задач таким же образом, как и IMalloc:: Alloc.
        [DllImport("ole32.dll")]
        internal static extern IntPtr CoTaskMemAlloc(int cb);
        //Изменяет размер ранее выделенного блока памяти задач.
        [DllImport("ole32.dll")]
        internal static extern IntPtr CoTaskMemRealloc(IntPtr pv, int cb);
        //Освобождает блок памяти задачи, ранее выделенный посредством вызова функции CoTaskMemAlloc или CoTaskMemRealloc.
        [DllImport("ole32.dll")]
        internal static extern void CoTaskMemFree(IntPtr ptr);
        //Задает режим ввода входного буфера консоли или режим вывода буфера экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        //Извлекает текущий режим ввода входного буфера консоли или текущий режим вывода буфера экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int mode);
        //Генерирует простые тона на динамике. Функция является синхронной; она выполняет предупреждающее ожидание и не возвращает управление вызывающему объекту до завершения звука.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Beep(int frequency, int duration);
        //Извлекает информацию об указанном буфере экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleScreenBufferInfo(
          IntPtr hConsoleOutput,
          out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);
        //Изменяет размер указанного буфера экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleScreenBufferSize(
          IntPtr hConsoleOutput,
          COORD size);
        //Извлекает размер максимально возможного окна консоли на основе текущего шрифта и размера дисплея.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern COORD GetLargestConsoleWindowSize(
          IntPtr hConsoleOutput);
        //Записывает символ в буфер экрана консоли заданное количество раз, начиная с указанных координат.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool FillConsoleOutputCharacter(
          IntPtr hConsoleOutput,
          char character,
          int nLength,
          COORD dwWriteCoord,
          out int pNumCharsWritten);
        //Задает атрибуты символов для указанного количества ячеек символов, начиная с указанных координат в буфере экрана.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool FillConsoleOutputAttribute(
          IntPtr hConsoleOutput,
          short wColorAttribute,
          int numCells,
          COORD startCoord,
          out int pNumBytesWritten);
        //Задает текущий размер и положение окна буфера экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe bool SetConsoleWindowInfo(
          IntPtr hConsoleOutput,
          bool absolute,
          SMALL_RECT* consoleWindow);
        //Задает атрибуты символов, записанных в буфер экрана консоли с помощью функции WriteFile или WriteConsole, или отраженных функцией ReadFile или ReadConsole. Эта функция влияет на текст, написанный после вызова функции.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, short attributes);
        //Задает положение курсора в указанном буфере экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleCursorPosition(
          IntPtr hConsoleOutput,
          COORD cursorPosition);
        //Задает размер и видимость курсора для указанного буфера экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleCursorInfo(
          IntPtr hConsoleOutput,
          out CONSOLE_CURSOR_INFO cci);
        //Задает размер и видимость курсора для указанного буфера экрана консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleCursorInfo(
          IntPtr hConsoleOutput,
          ref CONSOLE_CURSOR_INFO cci);
        //Извлекает заголовок для текущего окна консоли.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetConsoleTitle(StringBuilder sb, int capacity);
        //Задает заголовок текущего окна консоли.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetConsoleTitle(string title);
        //Считывает данные из входного буфера консоли и удаляет их из буфера.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadConsoleInput(
          IntPtr hConsoleInput,
          out InputRecord buffer,
          int numInputRecords_UseOne,
          out int numEventsRead);
        //Считывает данные из указанного входного буфера консоли, не удаляя их из буфера.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool PeekConsoleInput(
          IntPtr hConsoleInput,
          out InputRecord buffer,
          int numInputRecords_UseOne,
          out int numEventsRead);
        //Считывает данные атрибутов символов и цветов из прямоугольного блока ячеек символов в буфере экрана консоли, а функция записывает данные в прямоугольный блок в указанном месте в целевом буфере.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe bool ReadConsoleOutput(
          IntPtr hConsoleOutput,
          CHAR_INFO* pBuffer,
          COORD bufferSize,
          COORD bufferCoord,
          ref SMALL_RECT readRegion);
        //Записывает данные атрибутов символов и цветов в указанный прямоугольный блок ячеек символов в буфере экрана консоли. Записываемые данные берутся из прямоугольного блока соответствующего размера в указанном месте исходного буфера.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe bool WriteConsoleOutput(
          IntPtr hConsoleOutput,
          CHAR_INFO* buffer,
          COORD bufferSize,
          COORD bufferCoord,
          ref SMALL_RECT writeRegion);
        //Возвращает состояние указанного виртуального ключа. Статус указывает, является ли клавиша вверх, вниз или переключается (вкл, выкл-чередуется при каждом нажатии клавиши).
        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int virtualKeyCode);
        //Извлекает входную кодовую страницу, используемую консолью, связанной с вызывающим процессом. Консоль использует свою кодовую страницу ввода для перевода ввода с клавиатуры в соответствующее символьное значение.
        [DllImport("kernel32.dll")]
        internal static extern uint GetConsoleCP();
        //Задает входную кодовую страницу, используемую консолью, связанной с вызывающим процессом. Консоль использует свою кодовую страницу ввода для перевода ввода с клавиатуры в соответствующее символьное значение.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleCP(uint codePage);
        //Извлекает выходную кодовую страницу, используемую консолью, связанной с вызывающим процессом. Консоль использует свою выходную кодовую страницу для преобразования значений символов, записанных различными выходными функциями, в изображения, отображаемые в окне консоли.
        [DllImport("kernel32.dll")]
        internal static extern uint GetConsoleOutputCP();
        //Задает выходную кодовую страницу, используемую консолью, связанной с вызывающим процессом. Консоль использует свою выходную кодовую страницу для преобразования значений символов, записанных различными выходными функциями, в изображения, отображаемые в окне консоли.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleOutputCP(uint codePage);
        //Устанавливает соединение с предопределенным разделом реестра на другом компьютере.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegConnectRegistry(
          string machineName,
          SafeRegistryHandle key,
          out SafeRegistryHandle result);
        //Создает указанный раздел реестра. Если ключ уже существует, функция открывает его. Обратите внимание, что имена ключей не чувствительны к регистру.
        //Чтобы выполнить транзакционные операции реестра с ключом, вызовите функцию RegCreateKeyTransacted.
        //Приложения, выполняющие резервное копирование или восстановление состояния системы, включая системные файлы и кусты реестра, должны использовать службу теневого копирования томов вместо функций реестра.      
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegCreateKeyEx(
          SafeRegistryHandle hKey,
          string lpSubKey,
          int Reserved,
          string lpClass,
          int dwOptions,
          int samDesigner,
          SECURITY_ATTRIBUTES lpSecurityAttributes,
          out SafeRegistryHandle hkResult,
          out int lpdwDisposition);
        //Удаляет подраздел и его значения. Обратите внимание, что имена ключей не чувствительны к регистру.
        //64-разрядная версии Windows: на WoW64 32-битные приложения, просмотр дерева реестр, отдельный от реестра, которые 64-битные приложения.Чтобы разрешить приложению удалять запись в альтернативном представлении реестра, используйте функцию RegDeleteKeyEx.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegDeleteKey(SafeRegistryHandle hKey, string lpSubKey);
        //Удаляет именованное значение из указанного раздела реестра. Обратите внимание, что имена значений не чувствительны к регистру.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegDeleteValue(SafeRegistryHandle hKey, string lpValueName);
        //Перечисляет подразделы указанного открытого раздела реестра. Функция извлекает информацию об одном подразделе каждый раз, когда она вызывается.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegEnumKeyEx(
          SafeRegistryHandle hKey,
          int dwIndex,
          StringBuilder lpName,
          out int lpcbName,
          int[] lpReserved,
          StringBuilder lpClass,
          int[] lpcbClass,
          long[] lpftLastWriteTime);
        //Перечисляет значения для указанного открытого раздела реестра. Функция копирует одно индексированное имя значения и блок данных для ключа при каждом его вызове.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegEnumValue(
          SafeRegistryHandle hKey,
          int dwIndex,
          StringBuilder lpValueName,
          ref int lpcbValueName,
          IntPtr lpReserved_MustBeZero,
          int[] lpType,
          byte[] lpData,
          int[] lpcbData);
        ////Перечисляет значения для указанного открытого раздела реестра. Функция копирует одно индексированное имя значения и блок данных для ключа при каждом его вызове.
        [DllImport("advapi32.dll", CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int RegEnumValueA(
          SafeRegistryHandle hKey,
          int dwIndex,
          StringBuilder lpValueName,
          ref int lpcbValueName,
          IntPtr lpReserved_MustBeZero,
          int[] lpType,
          byte[] lpData,
          int[] lpcbData);
        //Записывает все атрибуты указанного открытого раздела реестра в реестр.
        [DllImport("advapi32.dll")]
        internal static extern int RegFlushKey(SafeRegistryHandle hKey);
        //Открывает указанный раздел реестра. Обратите внимание, что имена ключей не чувствительны к регистру.
        //Чтобы выполнить транзакционные операции реестра с ключом, вызовите функцию Regopenkeytransactedw.  
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegOpenKeyEx(
          SafeRegistryHandle hKey,
          string lpSubKey,
          int ulOptions,
          int samDesired,
          out SafeRegistryHandle hkResult);
        //Извлекает сведения об указанном разделе реестра.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegQueryInfoKey(
          SafeRegistryHandle hKey,
          StringBuilder lpClass,
          int[] lpcbClass,
          IntPtr lpReserved_MustBeZero,
          ref int lpcSubKeys,
          int[] lpcbMaxSubKeyLen,
          int[] lpcbMaxClassLen,
          ref int lpcValues,
          int[] lpcbMaxValueNameLen,
          int[] lpcbMaxValueLen,
          int[] lpcbSecurityDescriptor,
          int[] lpftLastWriteTime);
        //Извлекает тип и данные для указанного имени значения, связанного с открытым разделом реестра.
        //Чтобы убедиться, что все возвращаемые строковые значения(REG_SZ, REG_MULTI_SZ и REG_EXPAND_SZ) завершаются нулевым значением, используйте функцию RegGetValue.      
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegQueryValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int[] lpReserved,
          ref int lpType,
          [Out] byte[] lpData,
          ref int lpcbData);
        //Извлекает тип и данные для указанного имени значения, связанного с открытым разделом реестра.
        //Чтобы убедиться, что все возвращаемые строковые значения(REG_SZ, REG_MULTI_SZ и REG_EXPAND_SZ) завершаются нулевым значением, используйте функцию RegGetValue.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegQueryValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int[] lpReserved,
          ref int lpType,
          ref int lpData,
          ref int lpcbData);
        //Извлекает тип и данные для указанного имени значения, связанного с открытым разделом реестра.
        //Чтобы убедиться, что все возвращаемые строковые значения(REG_SZ, REG_MULTI_SZ и REG_EXPAND_SZ) завершаются нулевым значением, используйте функцию RegGetValue.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegQueryValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int[] lpReserved,
          ref int lpType,
          ref long lpData,
          ref int lpcbData);
        //Извлекает тип и данные для указанного имени значения, связанного с открытым разделом реестра.
        //Чтобы убедиться, что все возвращаемые строковые значения(REG_SZ, REG_MULTI_SZ и REG_EXPAND_SZ) завершаются нулевым значением, используйте функцию RegGetValue.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegQueryValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int[] lpReserved,
          ref int lpType,
          [Out] char[] lpData,
          ref int lpcbData);
        //Извлекает тип и данные для указанного имени значения, связанного с открытым разделом реестра.
        //Чтобы убедиться, что все возвращаемые строковые значения(REG_SZ, REG_MULTI_SZ и REG_EXPAND_SZ) завершаются нулевым значением, используйте функцию RegGetValue.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegQueryValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int[] lpReserved,
          ref int lpType,
          StringBuilder lpData,
          ref int lpcbData);
        //Задает данные и тип указанного значения в разделе реестра.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegSetValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int Reserved,
          RegistryValueKind dwType,
          byte[] lpData,
          int cbData);
        //Задает данные и тип указанного значения в разделе реестра.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegSetValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int Reserved,
          RegistryValueKind dwType,
          ref int lpData,
          int cbData);
        //Задает данные и тип указанного значения в разделе реестра.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegSetValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int Reserved,
          RegistryValueKind dwType,
          ref long lpData,
          int cbData);
        //Задает данные и тип указанного значения в разделе реестра.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int RegSetValueEx(
          SafeRegistryHandle hKey,
          string lpValueName,
          int Reserved,
          RegistryValueKind dwType,
          string lpData,
          int cbData);
        //Разворачивает исходную строку с помощью блока среды, установленного для указанного пользователя.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int ExpandEnvironmentStrings(
          string lpSrc,
          StringBuilder lpDst,
          int nSize);
        //Изменяет размер или атрибуты указанного объекта локальной памяти. Размер может увеличиваться или уменьшаться.
        [DllImport("kernel32.dll")]
        internal static extern IntPtr LocalReAlloc(
          IntPtr handle,
          IntPtr sizetcbBytes,
          int uFlags);
        //Извлекает полный путь к известной папке, определяемой идентификатором KNOWNFOLDERID папки. Это расширяет SHGetKnownFolderPath, позволяя установить начальный размер строкового буфера.
        [DllImport("shfolder.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int SHGetFolderPath(
          IntPtr hwndOwner,
          int nFolder,
          IntPtr hToken,
          int dwFlags,
          StringBuilder lpszPath);
        //Используйте функцию GetUserNameEx для получения имени пользователя в указанном формате. Дополнительная информация предоставляется интерфейсом IADsADSystemInfo.
        [DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern byte GetUserNameEx(
          int format,
          StringBuilder domainName,
          ref int domainNameLen);
        //Функция LookupAccountName принимает имя системы и учетной записи в качестве входных данных. Он извлекает идентификатор безопасности (SID) для учетной записи и имя домена, в котором была найдена учетная запись.
        //Функция LsaLookupNames также может извлекать учетные записи компьютеров.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool LookupAccountName(
          string machineName,
          string accountName,
          byte[] sid,
          ref int sidLen,
          StringBuilder domainName,
          ref int domainNameLen,
          out int peUse);
        //Возвращает дескриптор текущей станции окна для вызывающего процесса.
        [DllImport("user32.dll")]
        internal static extern IntPtr GetProcessWindowStation();
        //Задает информацию об указанном объекте window station или desktop.
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetUserObjectInformation(
          IntPtr hObj,
          int nIndex,
          [MarshalAs(UnmanagedType.LPStruct)] USEROBJECTFLAGS pvBuffer,
          int nLength,
          ref int lpnLengthNeeded);
        //Отправляет указанное сообщение в одно или несколько окон.
        [DllImport("user32.dll", SetLastError = true, BestFitMapping = false)]
        internal static extern IntPtr SendMessageTimeout(
          IntPtr hWnd,
          int Msg,
          IntPtr wParam,
          string lParam,
          uint fuFlags,
          uint uTimeout,
          IntPtr lpdwResult);
        //Функция LsaNtStatusToWinError преобразует код NTSTATUS, возвращаемый функцией LSA, в код ошибки Windows.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int LsaNtStatusToWinError([In] int status);
        //Функция BCryptGetFipsAlgorithmMode определяет, включено ли соответствие федеральному стандарту обработки информации (FIPS).
        [DllImport("bcrypt.dll")]
        internal static extern uint BCryptGetFipsAlgorithmMode([MarshalAs(UnmanagedType.U1)] out bool pfEnabled);
        //Функция AllocateLocallyUniqueId выделяет локально уникальный идентификатор (LUID).
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool AllocateLocallyUniqueId([In, Out] ref LUID Luid);
        //Функция ConvertSidToStringSid преобразует идентификатор безопасности (SID) в строковый формат, подходящий для отображения, хранения или передачи.
        //Чтобы преобразовать Sid строкового формата обратно в допустимую функциональную сторону, вызовите функцию ConvertStringSidToSid.        
        [DllImport("advapi32.dll", EntryPoint = "ConvertSecurityDescriptorToStringSecurityDescriptorW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int ConvertSdToStringSd(
          byte[] securityDescriptor,
          uint requestedRevision,
          uint securityInformation,
          out IntPtr resultString,
          ref uint resultStringLength);
        //Функция ConvertStringSidToSid преобразует строку-формат идентификатора безопасности (SID) в действительный и функциональный Сид. С помощью этой функции можно получить идентификатор безопасности, преобразованный функцией ConvertSidToStringSid в строковый формат.
        [DllImport("advapi32.dll", EntryPoint = "ConvertStringSecurityDescriptorToSecurityDescriptorW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int ConvertStringSdToSd(
          string stringSd,
          uint stringSdRevision,
          out IntPtr resultSd,
          ref uint resultSdLength);
        //Функция ConvertStringSidToSid преобразует строку-формат идентификатора безопасности (SID) в действительный и функциональный Сид. С помощью этой функции можно получить идентификатор безопасности, преобразованный функцией ConvertSidToStringSid в строковый формат.
        [DllImport("advapi32.dll", EntryPoint = "ConvertStringSidToSidW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int ConvertStringSidToSid(string stringSid, out IntPtr ByteArray);
        //Функция CreateWellKnownSid создает идентификатор безопасности для предопределенных псевдонимов.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int CreateWellKnownSid(
          int sidType,
          byte[] domainSid,
          [Out] byte[] resultSid,
          ref uint resultSidLength);
        //Возвращает значение, показывающее, относится ли идентификатор безопасности (ИД безопасности), представленный данным объектом SecurityIdentifier, к тому же домену, что и заданный ИД безопасности.
        [DllImport("advapi32.dll", EntryPoint = "EqualDomainSid", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int IsEqualDomainSid(byte[] sid1, byte[] sid2, out bool result);
        //Извлекает псевдо-дескриптор для текущего процесса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetCurrentProcess();
        //Функция GetSecurityDescriptorLength возвращает длину структурно допустимого дескриптора безопасности в байтах. Длина включает в себя длину всех связанных структур.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetSecurityDescriptorLength(IntPtr byteArray);

        [DllImport("advapi32.dll", EntryPoint = "GetSecurityInfo", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetSecurityInfoByHandle(
          SafeHandle handle,
          uint objectType,
          uint securityInformation,
          out IntPtr sidOwner,
          out IntPtr sidGroup,
          out IntPtr dacl,
          out IntPtr sacl,
          out IntPtr securityDescriptor);

        [DllImport("advapi32.dll", EntryPoint = "GetNamedSecurityInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetSecurityInfoByName(
          string name,
          uint objectType,
          uint securityInformation,
          out IntPtr sidOwner,
          out IntPtr sidGroup,
          out IntPtr dacl,
          out IntPtr sacl,
          out IntPtr securityDescriptor);
        //Функция GetWindowsAccountDomainSid получает идентификатор безопасности (SID) и возвращает идентификатор безопасности, представляющий домен этой стороны.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int GetWindowsAccountDomainSid(
          byte[] sid,
          [Out] byte[] resultSid,
          ref uint resultSidLength);
        //Функция IsWellKnownSid сравнивает сторону с известным SID и возвращает TRUE, если они совпадают.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int IsWellKnownSid(byte[] sid, int type);
        //Функция LookupPrivilegeValue извлекает локально уникальный идентификатор (LUID), используемый в указанной системе для локального представления указанного имени привилегии.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("advapi32.dll", EntryPoint = "LookupPrivilegeValueW", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool LookupPrivilegeValue(
          [In] string lpSystemName,
          [In] string lpName,
          [In, Out] ref LUID Luid);
        //Функция LsaFreeMemory освобождает память, выделенную для выходного буфера вызовом функции LSA. Функции LSA, возвращающие выходные буферы переменной длины, всегда выделяют буфер от имени вызывающего объекта. 
        //Вызывающий объект должен освободить эту память, передав возвращенный указатель буфера в LsaFreeMemory, когда память больше не требуется.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern int LsaFreeMemory(IntPtr handle);
        //Функция LsaDeregisterLogonProcess удаляет контекст приложения входа вызывающего пользователя и закрывает соединение с сервером LSA.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("secur32.dll", SetLastError = true)]
        internal static extern int LsaDeregisterLogonProcess(IntPtr handle);
        //Функция LsaClose закрывает дескриптор для объекта политики или TrustedDomain.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern int LsaClose(IntPtr handle);
        //Функция LsaFreeReturnBuffer освобождает память, используемую буфером, ранее выделенным LSA.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("secur32.dll", SetLastError = true)]
        internal static extern int LsaFreeReturnBuffer(IntPtr handle);

        [DllImport("advapi32.dll", EntryPoint = "SetNamedSecurityInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint SetSecurityInfoByName(
          string name,
          uint objectType,
          uint securityInformation,
          byte[] owner,
          byte[] group,
          byte[] dacl,
          byte[] sacl);
        //
        [DllImport("advapi32.dll", EntryPoint = "SetSecurityInfo", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint SetSecurityInfoByHandle(
          SafeHandle handle,
          uint objectType,
          uint securityInformation,
          byte[] owner,
          byte[] group,
          byte[] dacl,
          byte[] sacl);
        //Извлекает информацию о календаре для локали, указанной по имени.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
        internal static extern int GetCalendarInfo(
          int Locale,
          int Calendar,
          int CalType,
          StringBuilder lpCalData,
          int cchData,
          IntPtr lpValue);

        static Win32Api()
        {
            INVALID_HANDLE_VALUE = new IntPtr(-1);
            NULL = IntPtr.Zero;
        }
        //Содержит информацию о версии операционной системы. Эта информация включает в себя основные и второстепенные номера версий, номер сборки, идентификатор платформы и описательный текст об операционной системе. Эта структура используется с функцией GetVersionEx.
        //Чтобы получить дополнительную информацию о версии, используйте вместо этого структуру OSVERSIONINFOEX с GetVersionEx.       
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class OSVERSIONINFO
        {
            internal int OSVersionInfoSize;
            internal int MajorVersion;
            internal int MinorVersion;
            internal int BuildNumber;
            internal int PlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string CSDVersion;

            internal OSVERSIONINFO()
            {
                OSVersionInfoSize = Marshal.SizeOf((object)this);
            }
        }
        //Содержит информацию о версии операционной системы. Эти сведения включают номера основных и второстепенных версий, номер сборки, идентификатор платформы, а также сведения о наборах продуктов и последнем пакете обновления, установленном в системе. 
        //Эта структура используется с функциями GetVersionEx и VerifyVersionInfo.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class OSVERSIONINFOEX
        {
            internal int OSVersionInfoSize;
            internal int MajorVersion;
            internal int MinorVersion;
            internal int BuildNumber;
            internal int PlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string CSDVersion;
            internal ushort ServicePackMajor;
            internal ushort ServicePackMinor;
            internal short SuiteMask;
            internal byte ProductType;
            internal byte Reserved;

            public OSVERSIONINFOEX()
            {
                OSVersionInfoSize = Marshal.SizeOf((object)this);
            }
        }
        //Содержит информацию о текущей компьютерной системе. Это включает в себя архитектуру и тип процессора, количество процессоров в системе, размер страницы и другую подобную информацию.
        internal struct SYSTEM_INFO
        {
            internal int dwOemId;
            internal int dwPageSize;
            internal IntPtr lpMinimumApplicationAddress;
            internal IntPtr lpMaximumApplicationAddress;
            internal IntPtr dwActiveProcessorMask;
            internal int dwNumberOfProcessors;
            internal int dwProcessorType;
            internal int dwAllocationGranularity;
            internal short wProcessorLevel;
            internal short wProcessorRevision;
        }
        //Структура SECURITY_ATTRIBUTES содержит дескриптор безопасности для объекта и указывает, является ли дескриптор, полученный путем указания этой структуры, наследуемым. Эта структура предоставляет параметры безопасности для объектов, созданных различными функциями, такими как Createfilea, CreatePipe, CreateProcess, RegCreateKeyEx или RegSaveKeyEx.
        [StructLayout(LayoutKind.Sequential)]
        internal class SECURITY_ATTRIBUTES
        {
            internal int nLength;
            internal unsafe byte* pSecurityDescriptor;
            internal int bInheritHandle;

            public unsafe SECURITY_ATTRIBUTES()
            {
                this.pSecurityDescriptor = (byte*)null;
            }
        }

        //Содержит 64-разрядное значение, представляющее число 100-наносекундных интервалов с 1 января 1601 года (UTC).
        internal struct FILE_TIME
        {
            internal uint ftTimeLow;
            internal uint ftTimeHigh;

            public FILE_TIME(long fileTime)
            {
                ftTimeLow = (uint)fileTime;
                ftTimeHigh = (uint)(fileTime >> 32);
            }

            public long ToTicks()
            {
                return ((long)ftTimeHigh << 32) + ftTimeLow;
            }
        }
        //Структура KERBER_S4U_LOGON содержит сведения о службе для входа пользователя (S4U). Эта структура используется функцией LsaLogonUser с пакетом Kerberos.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct KERB_S4U_LOGON
        {
            internal uint MessageType;
            internal uint Flags;
            internal UNICODE_INTPTR_STRING ClientUpn;
            internal UNICODE_INTPTR_STRING ClientRealm;
        }
        //Структура LSA_OBJECT_ATTRIBUTES используется с функцией LsaOpenPolicy для указания атрибутов соединения с объектом политики.
        internal struct LSA_OBJECT_ATTRIBUTES
        {
            internal int Length;
            internal IntPtr RootDirectory;
            internal IntPtr ObjectName;
            internal int Attributes;
            internal IntPtr SecurityDescriptor;
            internal IntPtr SecurityQualityOfService;
        }
        //Структура UNICODE_STRING используется для определения строк Юникода.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct UNICODE_STRING
        {
            internal ushort Length;
            internal ushort MaximumLength;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string Buffer;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct UNICODE_INTPTR_STRING
        {
            internal ushort Length;
            internal ushort MaxLength;
            internal IntPtr Buffer;

            internal UNICODE_INTPTR_STRING(int length, int maximumLength, IntPtr buffer)
            {
                Length = (ushort)length;
                MaxLength = (ushort)maximumLength;
                Buffer = buffer;
            }
        }
        //Структура LSA_TRANSLATED_NAME используется с функцией LsaLookupSids для возврата информации об учетной записи, идентифицированной стороной.
        internal struct LSA_TRANSLATED_NAME
        {
            internal int Use;
            internal UNICODE_INTPTR_STRING Name;
            internal int DomainIndex;
        }
        //Структура LSA_TRANSLATED_SID используется с функцией LsaLookupNames для возврата сведений о идентификаторе безопасности, идентифицирующем учетную запись.
        internal struct LSA_TRANSLATED_SID
        {
            internal int Use;
            internal uint Rid;
            internal int DomainIndex;
        }
        //Структура LSA_TRANSLATED_SID2 содержит идентификаторы безопасности, которые извлекаются на основе имен учетных записей. Эта структура используется функцией LsaLookupNames2.
        internal struct LSA_TRANSLATED_SID2
        {
            internal int Use;
            internal IntPtr Sid;
            internal int DomainIndex;
            private uint Flags;
        }
        //Структура LSA_TRUST_INFORMATION определяет домен.
        internal struct LSA_TRUST_INFORMATION
        {
            internal UNICODE_INTPTR_STRING Name;
            internal IntPtr Sid;
        }
        //Структура LSA_REFERENCED_DOMAIN_LIST содержит сведения о доменах, на которые ссылается операция поиска.
        internal struct LSA_REFERENCED_DOMAIN_LIST
        {
            internal int Entries;
            internal IntPtr Domains;
        }
        //Представляет локально уникальный идентификатор (LUID)
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct LUID
        {
            internal uint LowPart;
            internal uint HighPart;
        }
        //FLUID_AND_ATTRIBUTES представляет локально уникальный идентификатор (LUID) и его атрибуты.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct LUID_AND_ATTRIBUTES
        {
            internal LUID Luid;
            internal uint Attributes;
        }
        //Структура QUOTA_LIMITS описывает объем системных ресурсов, доступных пользователю.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct QUOTA_LIMITS
        {
            internal IntPtr PagedPoolLimit;
            internal IntPtr NonPagedPoolLimit;
            internal IntPtr MinimumWorkingSetSize;
            internal IntPtr MaximumWorkingSetSize;
            internal IntPtr PagefileLimit;
            internal IntPtr TimeLimit;
        }
        //Структура SECURITY_LOGON_SESSION_DATA содержит сведения о сеансе входа в систему.
        //Эта структура используется функцией LsaGetLogonSessionData.       
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct SECURITY_LOGON_SESSION_DATA
        {
            internal uint Size;
            internal LUID LogonId;
            internal UNICODE_INTPTR_STRING UserName;
            internal UNICODE_INTPTR_STRING LogonDomain;
            internal UNICODE_INTPTR_STRING AuthenticationPackage;
            internal uint LogonType;
            internal uint Session;
            internal IntPtr Sid;
            internal long LogonTime;
        }
        //Структура SID_AND_ATTRIBUTES представляет идентификатор безопасности (SID) и его атрибуты. Идентификаторы безопасности используются для уникальной идентификации пользователей или групп.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct SID_AND_ATTRIBUTES
        {
            internal IntPtr Sid;
            internal uint Attributes;
        }
        //Структура TOKEN_GROUPS содержит информацию о идентификаторах безопасности группы (Sid) в маркере доступа.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TOKEN_GROUPS
        {
            internal uint GroupCount;
            internal SID_AND_ATTRIBUTES Groups;
        }
        //Структура TOKEN_PRIVILEGES содержит информацию о наборе привилегий для маркера доступа.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TOKEN_PRIVILEGE
        {
            internal uint PrivilegeCount;
            internal LUID_AND_ATTRIBUTES Privilege;
        }
        //Структура TOKEN_SOURCE определяет источник маркера доступа.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TOKEN_SOURCE
        {
            private const int TOKEN_SOURCE_LENGTH = 8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            internal char[] Name;
            internal LUID SourceIdentifier;
        }
        //Структура TOKEN_STATISTICS содержит информацию о маркере доступа. Приложение может получить эту информацию, вызвав функцию GetTokenInformation.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TOKEN_STATISTICS
        {
            internal LUID TokenId;
            internal LUID AuthenticationId;
            internal long ExpirationTime;
            internal uint TokenType;
            internal uint ImpersonationLevel;
            internal uint DynamicCharged;
            internal uint DynamicAvailable;
            internal uint GroupCount;
            internal uint PrivilegeCount;
            internal LUID ModifiedId;
        }
        //Структура TOKEN_USER идентифицирует пользователя, связанного с маркером доступа.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TOKEN_USER
        {
            internal SID_AND_ATTRIBUTES User;
        }
        //Содержит информацию о текущем состоянии физической и виртуальной памяти, включая расширенную память. Функция GlobalMemoryStatusEx хранит информацию в этой структуре.
        [StructLayout(LayoutKind.Sequential)]
        internal class MEMORYSTATUSEX
        {
            internal int length;
            internal int memoryLoad;
            internal ulong totalPhys;
            internal ulong availPhys;
            internal ulong totalPageFile;
            internal ulong availPageFile;
            internal ulong totalVirtual;
            internal ulong availVirtual;
            internal ulong availExtendedVirtual;

            internal MEMORYSTATUSEX()
            {
                this.length = Marshal.SizeOf((object)this);
            }
        }
        // //Содержит информацию о текущем состоянии физической и виртуальной памяти, включая расширенную память.
        [StructLayout(LayoutKind.Sequential)]
        internal class MEMORYSTATUS
        {
            internal int length;
            internal int memoryLoad;
            internal uint totalPhys;
            internal uint availPhys;
            internal uint totalPageFile;
            internal uint availPageFile;
            internal uint totalVirtual;
            internal uint availVirtual;

            internal MEMORYSTATUS()
            {
                this.length = Marshal.SizeOf((object)this);
            }
        }
        //Содержит сведения о диапазоне страниц в виртуальном адресном пространстве процесса. Функции VirtualQuery и VirtualQueryEx используют эту структуру.
        internal struct MEMORY_BASIC_INFORMATION
        {
            internal unsafe void* BaseAddress;
            internal unsafe void* AllocationBase;
            internal uint AllocationProtect;
            internal UIntPtr RegionSize;
            internal uint State;
            internal uint Protect;
            internal uint Type;
        }
        //Содержит информацию о файле, найденном функцией FindFirstFile, FindFirstFileEx или FindNextFile.
        [BestFitMapping(false)]
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class WIN32_FIND_DATA
        {
            internal int dwFileAttributes;
            internal int ftCreationTime_dwLowDateTime;
            internal int ftCreationTime_dwHighDateTime;
            internal int ftLastAccessTime_dwLowDateTime;
            internal int ftLastAccessTime_dwHighDateTime;
            internal int ftLastWriteTime_dwLowDateTime;
            internal int ftLastWriteTime_dwHighDateTime;
            internal int nFileSizeHigh;
            internal int nFileSizeLow;
            internal int dwReserved0;
            internal int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            internal string cAlternateFileName;

            public WIN32_FIND_DATA() { }
        }

        internal delegate bool ConsoleCtrlHandlerRoutine(int controlType);
        //Определяет координаты символьной ячейки в буфере экрана консоли. Начало координат системы координат (0,0)находится в верхней, левой ячейке буфера.
        internal struct COORD
        {
            internal short X;
            internal short Y;
        }
        //Определяет координаты верхнего левого и нижнего правого углов прямоугольника.
        internal struct SMALL_RECT
        {
            internal short Left;
            internal short Top;
            internal short Right;
            internal short Bottom;
        }
        //Содержит информацию о буфере экрана консоли.
        internal struct CONSOLE_SCREEN_BUFFER_INFO
        {
            internal COORD dwSize;
            internal COORD dwCursorPosition;
            internal short wAttributes;
            internal SMALL_RECT srWindow;
            internal COORD dwMaximumWindowSize;
        }
        //Содержит информацию о курсоре консоли.
        internal struct CONSOLE_CURSOR_INFO
        {
            internal int dwSize;
            internal bool bVisible;
        }
        //Структура KEY_EVENT_RECORD используется для записи событий ввода с клавиатуры в структуре INPUT_RECORD консоли.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct KeyEventRecord
        {
            internal bool keyDown;
            internal short repeatCount;
            internal short virtualKeyCode;
            internal short virtualScanCode;
            internal char uChar;
            internal int controlKeyState;
        }
        //Описывает входное событие в буфере ввода консоли. Эти записи могут быть считаны из входного буфера с помощью функции ReadConsoleInput или PeekConsoleInput или записаны во входной буфер с помощью функции WriteConsoleInput.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct InputRecord
        {
            internal short eventType;
            internal KeyEventRecord keyEvent;
        }
        //Представляет цвет ARGB
        [Flags]
        [Serializable]
        internal enum Color : short
        {
            Black = 0,
            ForegroundBlue = 1,
            ForegroundGreen = 2,
            ForegroundRed = 4,
            ForegroundYellow = ForegroundRed | ForegroundGreen, // 0x0006
            ForegroundIntensity = 8,
            BackgroundBlue = 16, // 0x0010
            BackgroundGreen = 32, // 0x0020
            BackgroundRed = 64, // 0x0040
            BackgroundYellow = BackgroundRed | BackgroundGreen, // 0x0060
            BackgroundIntensity = 128, // 0x0080
            ForegroundMask = ForegroundIntensity | ForegroundYellow | ForegroundBlue, // 0x000F
            BackgroundMask = BackgroundIntensity | BackgroundYellow | BackgroundBlue, // 0x00F0
            ColorMask = BackgroundMask | ForegroundMask, // 0x00FF
        }
        //Задает символ Unicode или ANSI и его атрибуты. Эта структура используется консольными функциями для чтения и записи в буфер экрана консоли.
        internal struct CHAR_INFO
        {
            private ushort charData;
            private short attributes;
        }
        //Содержит информацию о дескрипторе оконной станции или рабочего стола.
        [StructLayout(LayoutKind.Sequential)]
        internal class USEROBJECTFLAGS
        {
            internal int fInherit;
            internal int fReserved;
            internal int dwFlags;

            public USEROBJECTFLAGS() { }
        }
        //Перечисление SECURITY_IMPERSONATION_LEVEL содержит значения, указывающие уровни олицетворения безопасности. Уровни олицетворения безопасности определяют степень, в которой серверный процесс может действовать от имени клиентского процесса.
        internal enum SECURITY_IMPERSONATION_LEVEL
        {
            //Cерверный процесс не может получить идентификационную информацию о клиенте, и он не может олицетворять клиента. Он определяется без заданного значения и, таким образом, по правилам ANSI C, по умолчанию имеет нулевое значение.
            Anonymous,
            //Процесс сервера может получать информацию о клиенте, например идентификаторы безопасности и привилегии, но не может олицетворять клиента. Это полезно для серверов, экспортирующих собственные объекты, например продукты баз данных, экспортирующие таблицы и представления. 
            //Используя полученную информацию о безопасности клиента, сервер может принимать решения о проверке доступа, не имея возможности использовать другие службы, использующие контекст безопасности клиента.
            Identification,
            //Cерверный процесс может олицетворять контекст безопасности клиента в своей локальной системе. Сервер не может олицетворять клиента в удаленных системах.
            Impersonation,
            //Процесс сервера может олицетворять контекст безопасности клиента в удаленных системах.
            Delegation,
        }
    }
    /// <summary>
    /// Api для работы с файлами
    /// </summary>
    internal static class FileApi
    {
        //Извлекает полный путь и имя указанного файла.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetFullPathName(
          [In] char[] path,
          int numBufferChars,
          [Out] char[] buffer,
          IntPtr mustBeZero);
        //Извлекает полный путь и имя указанного файла.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern unsafe int GetFullPathName(
          char* path,
          int numBufferChars,
          char* buffer,
          IntPtr mustBeZero);
        //Преобразует указанный путь в его длинную форму.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetLongPathName(
          string path,
          StringBuilder longPathBuffer,
          int bufferLength);
        //Преобразует указанный путь в его длинную форму.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetLongPathName(
          [In] char[] path,
          [Out] char[] longPathBuffer,
          int bufferLength);
        //Преобразует указанный путь в его длинную форму.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern unsafe int GetLongPathName(
          char* path,
          char* longPathBuffer,
          int bufferLength);

        internal static SafeFileHandle UnsafeCreateFile(
          string lpFileName,
          int dwDesiredAccess,
          FileShare dwShareMode,
          SECURITY_ATTRIBUTES securityAttrs,
          FileMode dwCreationDisposition,
          int dwFlagsAndAttributes,
          IntPtr hTemplateFile)
        {
            return CreateFile(lpFileName, dwDesiredAccess, dwShareMode, securityAttrs, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
        }
        //Создает или открывает файл или устройство ввода-вывода. Наиболее часто используемые устройства ввода-вывода: файл, поток файлов, каталог, физический диск, том, буфер консоли, ленточный накопитель, коммуникационный ресурс, почтовый ящик и канал. 
        //Функция возвращает дескриптор, который может использоваться для доступа к файлу или устройству для различных типов ввода-вывода в зависимости от файла или устройства и указанных флагов и атрибутов.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        private static extern SafeFileHandle CreateFile(
          string lpFileName,
          int dwDesiredAccess,
          FileShare dwShareMode,
          SECURITY_ATTRIBUTES securityAttrs,
          FileMode dwCreationDisposition,
          int dwFlagsAndAttributes,
          IntPtr hTemplateFile);
        //Отменяет отображение сопоставленного представления файла из адресного пространства вызывающего процесса.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll")]
        internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);
        //Извлекает тип файла указанного файла.
        [DllImport("kernel32.dll")]
        internal static extern int GetFileType(SafeFileHandle handle);
        //Задает физический размер файла для указанного файла в текущей позиции указателя файла.
        //Физический размер файла также называется концом файла.Функция SetEndOfFile может использоваться для усечения или расширения файла.Чтобы задать логический конец файла, используйте функцию SetFileValidData.    
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetEndOfFile(SafeFileHandle hFile);

        [DllImport("kernel32.dll", EntryPoint = "SetFilePointer", SetLastError = true)]
        private static extern unsafe int SetFilePointerWin32(
          SafeFileHandle handle,
          int lo,
          int* hi,
          int origin);
        //Перемещает указатель на указанный файл.
        //Эта функция сохраняет указатель файла в двух длинных значениях.Для работы с указателями файлов, размер которых превышает одно длинное значение, проще использовать функцию SetFilePointerEx.
        internal static unsafe long SetFilePointer(
          SafeFileHandle handle,
          long offset,
          SeekOrigin origin,
          out int hr)
        {
            hr = 0;
            int lo = (int)offset;
            int num1 = (int)(offset >> 32);
            int num2 = SetFilePointerWin32(handle, lo, &num1, (int)origin);
            if (num2 == -1 && (hr = Marshal.GetLastWin32Error()) != 0)
                return -1;
            return (long)(uint)num1 << 32 | (long)(uint)num2;
        }
        //Считывает данные из указанного файла или устройства ввода-вывода. Чтение происходит в позиции, указанной указателем файла, если поддерживается устройством.
        //Эта функция предназначена как для синхронных, так и для асинхронных операций.      
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe int ReadFile(
          SafeFileHandle handle,
          byte* bytes,
          int numBytesToRead,
          IntPtr numBytesRead_mustBeZero,
          NativeOverlapped* overlapped);
        //Считывает данные из указанного файла или устройства ввода-вывода. Чтение происходит в позиции, указанной указателем файла, если поддерживается устройством.
        //Эта функция предназначена как для синхронных, так и для асинхронных операций.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe int ReadFile(
          SafeFileHandle handle,
          byte* bytes,
          int numBytesToRead,
          out int numBytesRead,
          IntPtr mustBeZero);
        //Записывает данные в указанный файл или устройство ввода - вывода.
        //Эта функция предназначена как для синхронной, так и для асинхронной работы.       
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe int WriteFile(
          SafeFileHandle handle,
          byte* bytes,
          int numBytesToWrite,
          IntPtr numBytesWritten_mustBeZero,
          NativeOverlapped* lpOverlapped);
        //Записывает данные в указанный файл или устройство ввода - вывода.
        //Эта функция предназначена как для синхронной, так и для асинхронной работы. 
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe int WriteFile(
          SafeFileHandle handle,
          byte* bytes,
          int numBytesToWrite,
          out int numBytesWritten,
          IntPtr mustBeZero);
        //Извлекает путь к системному каталогу. Системный каталог содержит системные файлы, такие как динамические библиотеки и драйверы.
        //Эта функция предоставляется в первую очередь для обеспечения совместимости.Приложения должны хранить код в папке Program Files и постоянные данные в папке Application Data в профиле пользователя.       
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetSystemDirectory(StringBuilder sb, int length);
        //Задает дату и время создания, последнего доступа или последнего изменения указанного файла или каталога.
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern unsafe bool SetFileTime(
          SafeFileHandle hFile,
          FILE_TIME* creationTime,
          FILE_TIME* lastAccessTime,
          FILE_TIME* lastWriteTime);
        //Возвращает размер указанного файла в байтах.
        //Рекомендуется использовать GetFileSizeEx.       
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int GetFileSize(SafeFileHandle hFile, out int highSize);
        //Блокирует указанный файл для монопольного доступа вызывающим процессом.
        //Чтобы задать дополнительные параметры, например создание общей блокировки или операции блокировки при сбое, используйте функцию LockFileEx.       
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool LockFile(
          SafeFileHandle handle,
          int offsetLow,
          int offsetHigh,
          int countLow,
          int countHigh);
        //Открывает область в открытом файле. Разблокирование региона позволяет другим процессам получить к нему доступ.
        //Для альтернативного способа указания региона используйте функцию UnlockFileEx.       
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool UnlockFile(
          SafeFileHandle handle,
          int offsetLow,
          int offsetHigh,
          int countLow,
          int countHigh);
        //Копирует существующий файл в новый файл.
        //Функция CopyFileEx предоставляет две дополнительные возможности.CopyFileEx может вызывать указанную функцию обратного вызова каждый раз, когда часть операции копирования завершена, и CopyFileEx может быть отменен во время операции копирования.
        //Чтобы выполнить эту операцию как транзакционную, используйте функцию CopyFileTransacted.    
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool CopyFile(string src, string dst, bool failIfExists);
        //Создает новый каталог. Если базовая файловая система поддерживает защиту файлов и каталогов, функция применяет указанный дескриптор безопасности к новому каталогу.
        //Чтобы указать каталог шаблонов, используйте функцию Createdirectory.
        //Чтобы выполнить эту операцию как транзакционную, используйте функцию CreateDirectoryTransacted.      
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool CreateDirectory(
          string path,
          SECURITY_ATTRIBUTES lpSecurityAttributes);
        //Удаляет существующий файл.
        //Чтобы выполнить эту операцию как транзакционную, используйте функцию DeleteFileTransacted.        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool DeleteFile(string path);
        //Заменяет один файл другим файлом, с возможностью создания резервной копии исходного файла. Файл замены принимает имя замененного файла и его идентификатор.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool ReplaceFile(
          string replacedFileName,
          string replacementFileName,
          string backupFileName,
          int dwReplaceFlags,
          IntPtr lpExclude,
          IntPtr lpReserved);
        //Расшифровывает зашифрованный файл или каталог.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool DecryptFile(string path, int reservedMustBeZero);
        //Шифрует файл или каталог. Все потоки данных в файле зашифрованы. Все новые файлы, созданные в зашифрованном каталоге, шифруются.
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool EncryptFile(string path);
        //Закрывает дескриптор поиска файлов, открытый функциями FindFirstFile, FindFirstFileEx, FindFirstFileNameW, FindFirstFileNameTransactedW, FindFirstFileTransacted, FindFirstStreamTransactedW или FindFirstStreamW.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll")]
        internal static extern bool FindClose(IntPtr handle);
        //Извлекает текущий каталог для текущего процесса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern int GetCurrentDirectory(int nBufferLength, StringBuilder lpBuffer);
        //Извлекает атрибуты для указанного файла или каталога.
        //Чтобы выполнить эту операцию как транзакционную, используйте функцию Getfileattributestransactedw.     
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool GetFileAttributesEx(
          string name,
          int fileInfoLevel,
          ref WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);
        //Задает атрибуты для файла или каталога.
        //Чтобы выполнить эту операцию как транзакционную операцию, используйте функцию задать атрибуты файла Transacted.       
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool SetFileAttributes(string name, int attr);
        //Создает имя для временного файла. Если создается уникальное имя файла, создается пустой файл и дескриптор для него освобождается; в противном случае создается только имя файла.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern uint GetTempFileName(
          string tmpPath,
          string prefix,
          uint uniqueIdOrZero,
          StringBuilder tmpFileName);
        //Перемещает существующий файл или каталог, включая его дочерние элементы.
        //Чтобы указать способ перемещения файла, используйте функцию MoveFileEx или MoveFileWithProgress.
        //Чтобы выполнить эту операцию как транзакционную, используйте функцию MoveFileTransacted.     
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool MoveFile(string src, string dst);
        //Удаляет существующий пустой каталог.
        //Чтобы выполнить эту операцию как транзакционную операцию, используйте функцию RemoveDirectoryTransacted .
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool RemoveDirectory(string path);
        //Изменяет текущий каталог для текущего процесса.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern bool SetCurrentDirectory(string path);
        //Создает или открывает именованный или неназванный объект сопоставления файлов для указанного файла.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [SecurityCritical]
        internal static extern SafeMemoryMappedFileHandle CreateFileMapping(
                            SafeFileHandle hFile,
                            SECURITY_ATTRIBUTES lpAttributes,
                            int fProtect,
                            int dwMaximumSizeHigh,
                            int dwMaximumSizeLow,
                            String lpName
                            );
        //Открывает именованный объект сопоставления файлов.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [SecurityCritical]
        internal static extern SafeMemoryMappedFileHandle OpenFileMapping(
                            int dwDesiredAccess,
                            [MarshalAs(UnmanagedType.Bool)]
                                bool bInheritHandle,
                            string lpName
                            );
        //Отображение представления сопоставления файлов в адресное пространство вызывающего процесса.
        //Чтобы указать рекомендуемый базовый адрес для представления, используйте функцию MapViewOfFileEx.Однако такая практика не рекомендуется.

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        [SecurityCritical]
        internal static extern SafeMemoryMappedViewHandle MapViewOfFile(
                            SafeMemoryMappedFileHandle handle,
                            int dwDesiredAccess,
                            uint dwFileOffsetHigh,
                            uint dwFileOffsetLow,
                            UIntPtr dwNumberOfBytesToMap
                            );
        //Отмечает все невыполненные операции ввода-вывода для указанного дескриптора файла. Функция отменяет только операции ввода-вывода в текущем процессе, независимо от того, какой поток создал операцию ввода-вывода.
        [DllImport("kernel32.dll", SetLastError = true)]
        [SecurityCritical]
        internal static unsafe extern bool CancelIoEx(SafeHandle handle, NativeOverlapped* lpOverlapped);

        //Структура SECURITY_ATTRIBUTES содержит дескриптор безопасности для объекта и указывает, является ли дескриптор, полученный путем указания этой структуры, наследуемым. Эта структура предоставляет параметры безопасности для объектов, созданных различными функциями, такими как Createfilea, CreatePipe, CreateProcess, RegCreateKeyEx или RegSaveKeyEx.
        [StructLayout(LayoutKind.Sequential)]
        internal class SECURITY_ATTRIBUTES
        {
            internal int nLength;
            internal unsafe byte* pSecurityDescriptor;
            internal int bInheritHandle;

            public unsafe SECURITY_ATTRIBUTES()
            {
                this.pSecurityDescriptor = (byte*)null;
            }
        }
        //Содержит атрибутивную информацию для файла или каталога. Функция GetFileAttributesEx использует эту структуру.
        [Serializable]
        internal struct WIN32_FILE_ATTRIBUTE_DATA
        {
            internal int fileAttributes;
            internal uint ftCreationTimeLow;
            internal uint ftCreationTimeHigh;
            internal uint ftLastAccessTimeLow;
            internal uint ftLastAccessTimeHigh;
            internal uint ftLastWriteTimeLow;
            internal uint ftLastWriteTimeHigh;
            internal int fileSizeHigh;
            internal int fileSizeLow;
        }
        //Содержит 64-разрядное значение, представляющее число 100-наносекундных интервалов с 1 января 1601 года (UTC).
        internal struct FILE_TIME
        {
            internal uint ftTimeLow;
            internal uint ftTimeHigh;

            public FILE_TIME(long fileTime)
            {
                ftTimeLow = (uint)fileTime;
                ftTimeHigh = (uint)(fileTime >> 32);
            }

            public long ToTicks()
            {
                return ((long)ftTimeHigh << 32) + ftTimeLow;
            }
        }

    }
    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeApi
    {
        internal const int ERROR_HANDLE_EOF = 38;
        internal const int ERROR_INVALID_DRIVE = 15;
        internal const int ERROR_NO_MORE_FILES = 18;
        internal const int ERROR_NOT_READY = 21;
        internal const int ERROR_BAD_LENGTH = 24;
        internal const int ERROR_SHARING_VIOLATION = 32;
        internal const int ERROR_FILE_EXISTS = 80;
        internal const int ERROR_OPERATION_ABORTED = 995;
        internal const int ERROR_INVALID_HANDLE = 6;
        internal const int ERROR_INVALID_NAME = 123;
        internal const int ERROR_BAD_PATHNAME = 161;
        internal const int ERROR_ALREADY_EXISTS = 183;
        internal const int ERROR_ENVVAR_NOT_FOUND = 203;
        internal const int ERROR_FILENAME_EXCED_RANGE = 206;
        internal const int ERROR_MORE_DATA = 234;
        internal const int ERROR_NOT_FOUND = 1168;      
      
        internal static readonly IntPtr NULL;
        //Извлекает путь ко всем языковым файлам ресурсов, связанным с предоставленным файлом LN. Приложение должно вызывать эту функцию несколько раз, чтобы получить путь к каждому файлу ресурсов.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetFileMUIPath(
          int flags,
          [MarshalAs(UnmanagedType.LPWStr)] string filePath,
          [MarshalAs(UnmanagedType.LPWStr)] StringBuilder language,
          ref int languageLength,
          [MarshalAs(UnmanagedType.LPWStr)] StringBuilder fileMuiPath,
          ref int fileMuiPathLength,
          ref long enumerator);
        //Освобождает загруженный модуль динамической библиотеки (DLL) и при необходимости уменьшает его количество ссылок. Когда счетчик ссылок достигает нуля, модуль выгружается из адресного пространства вызывающего процесса, и дескриптор становится недействительным.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);
        //Дублирует дескриптор объекта.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DuplicateHandle(
          IntPtr hSourceProcessHandle,
          SafePipeHandle hSourceHandle,
          IntPtr hTargetProcessHandle,
          out SafePipeHandle lpTargetHandle,
          uint dwDesiredAccess,
          [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
          uint dwOptions);
        //Создает анонимный канал и возвращает дескрипторы для чтения и записи концов канала.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CreatePipe(
          out SafePipeHandle hReadPipe,
          out SafePipeHandle hWritePipe,
          SECURITY_ATTRIBUTES lpPipeAttributes,
          int nSize);
        //Создает экземпляр именованного канала и возвращает дескриптор для последующих операций канала. Процесс сервера именованных каналов использует эту функцию либо для создания первого экземпляра определенного именованного канала и установления его основных атрибутов, либо для создания нового экземпляра существующего именованного канала.
        [DllImport("kernel32.dll", EntryPoint = "CreateFile", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern SafePipeHandle CreateNamedPipeClient(
          string lpFileName,
          int dwDesiredAccess,
          FileShare dwShareMode,
          SECURITY_ATTRIBUTES securityAttrs,
          FileMode dwCreationDisposition,
          int dwFlagsAndAttributes,
          IntPtr hTemplateFile);
        //Позволяет серверному процессу именованного канала ожидать подключения клиентского процесса к экземпляру именованного канала. Клиентский процесс подключается путем вызова функции CreateFile или CallNamedPipe.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool ConnectNamedPipe(
          SafePipeHandle handle,
          NativeOverlapped* overlapped);
        //Позволяет серверному процессу именованного канала ожидать подключения клиентского процесса к экземпляру именованного канала. Клиентский процесс подключается путем вызова функции CreateFile или CallNamedPipe.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ConnectNamedPipe(SafePipeHandle handle, IntPtr overlapped);
        //Ожидает, пока не истечет интервал ожидания или экземпляр указанного именованного канала не будет доступен для подключения (т. е. серверный процесс канала имеет ожидающую операцию ConnectNamedPipe на канале).
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WaitNamedPipe(string name, int timeout);
        //Извлекает сведения об указанном именованном канале. Возвращаемая информация может изменяться в течение времени существования экземпляра именованного канала.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeHandleState(
          SafePipeHandle hNamedPipe,
          out int lpState,
          IntPtr lpCurInstances,
          IntPtr lpMaxCollectionCount,
          IntPtr lpCollectDataTimeout,
          IntPtr lpUserName,
          int nMaxUserNameSize);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeHandleState(
          SafePipeHandle hNamedPipe,
          IntPtr lpState,
          out int lpCurInstances,
          IntPtr lpMaxCollectionCount,
          IntPtr lpCollectDataTimeout,
          IntPtr lpUserName,
          int nMaxUserNameSize);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeHandleState(
          SafePipeHandle hNamedPipe,
          IntPtr lpState,
          IntPtr lpCurInstances,
          IntPtr lpMaxCollectionCount,
          IntPtr lpCollectDataTimeout,
          StringBuilder lpUserName,
          int nMaxUserNameSize);
        //Извлекает сведения об указанном именованном канале.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeInfo(
          SafePipeHandle hNamedPipe,
          out int lpFlags,
          IntPtr lpOutBufferSize,
          IntPtr lpInBufferSize,
          IntPtr lpMaxInstances);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeInfo(
          SafePipeHandle hNamedPipe,
          IntPtr lpFlags,
          out int lpOutBufferSize,
          IntPtr lpInBufferSize,
          IntPtr lpMaxInstances);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeInfo(
          SafePipeHandle hNamedPipe,
          IntPtr lpFlags,
          IntPtr lpOutBufferSize,
          out int lpInBufferSize,
          IntPtr lpMaxInstances);
        //Задает режим чтения и режим блокировки указанного именованного канала. Если указанный дескриптор относится к клиентскому концу именованного канала и если процесс сервера именованного канала находится на удаленном компьютере, эта функция также может использоваться для управления локальной буферизацией.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool SetNamedPipeHandleState(
          SafePipeHandle hNamedPipe,
          int* lpMode,
          IntPtr lpMaxCollectionCount,
          IntPtr lpCollectDataTimeout);
        //Отключает серверный конец экземпляра именованного канала от клиентского процесса.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DisconnectNamedPipe(SafePipeHandle hNamedPipe);
        //Очищает буферы указанного файла и вызывает запись всех буферизованных данных в файл.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FlushFileBuffers(SafePipeHandle hNamedPipe);
        //Функция RevertToSelf завершает олицетворение клиентского приложения.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RevertToSelf();
        //Функция ImpersonateNamedPipeClient олицетворяет клиентское приложение именованного канала.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ImpersonateNamedPipeClient(SafePipeHandle hNamedPipe);
        //Создает экземпляр именованного канала и возвращает дескриптор для последующих операций канала. Процесс сервера именованных каналов использует эту функцию либо для создания первого экземпляра определенного именованного канала и установления его основных атрибутов, либо для создания нового экземпляра существующего именованного канала.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        internal static extern SafePipeHandle CreateNamedPipe(
          string pipeName,
          int openMode,
          int pipeMode,
          int maxInstances,
          int outBufferSize,
          int inBufferSize,
          int defaultTimeout,
          SECURITY_ATTRIBUTES securityAttributes);
        //Регистрирует провайдера.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe uint EventRegister(
          [In] ref Guid providerId,
          [In] EtwEnableCallback enableCallback,
          [In] void* callbackContext,
          [In, Out] ref long registrationHandle);
        //Удаляет регистрацию поставщика. Эту функцию необходимо вызвать до завершения процесса.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern int EventUnregister([In] long registrationHandle);
        //Определяет, включено ли событие для любого сеанса.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern int EventEnabled(
          [In] long registrationHandle,
          [In] ref EventDescriptor eventDescriptor);
        //Определяет, включено ли событие для любого сеанса.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern int EventProviderEnabled(
          [In] long registrationHandle,
          [In] byte level,
          [In] long keywords);
        //Используйте эту функцию для записи события.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe uint EventWrite(
          [In] long registrationHandle,
          [In] ref EventDescriptor eventDescriptor,
          [In] uint userDataCount,
          [In] void* userData);
        //Используйте эту функцию для записи события.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe uint EventWrite(
          [In] long registrationHandle,
          [In] EventDescriptor* eventDescriptor,
          [In] uint userDataCount,
          [In] void* userData);
        //Связывает события вместе при трассировке событий в сквозном сценарии.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe uint EventWriteTransfer(
          [In] long registrationHandle,
          [In] ref EventDescriptor eventDescriptor,
          [In] ref Guid activityId,
          [In] ref Guid relatedActivityId,
          [In] uint userDataCount,
          [In] void* userData);
        //Записывает событие, содержащее строку в качестве своих данных.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern unsafe uint EventWriteString(
          [In] long registrationHandle,
          [In] byte level,
          [In] long keywords,
          [In] char* message);
        //Создает, запрашивает и задает текущий идентификатор действия, используемый функцией EventWriteTransfer.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint EventActivityIdControl([In] int ControlCode, [In, Out] ref Guid ActivityId);
        //Удаляет регистрацию поставщика из списка зарегистрированных поставщиков и освобождает все ресурсы, связанные с поставщиком.
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint PerfStopProvider([In] IntPtr hProvider);
        //Закрывает открытую ручку.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("wevtapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EvtClose(IntPtr handle);

        static UnsafeApi()
        {
            NULL = IntPtr.Zero;
        }
        //Структура SECURITY_ATTRIBUTES содержит дескриптор безопасности для объекта и указывает, является ли дескриптор, полученный путем указания этой структуры, наследуемым. Эта структура предоставляет параметры безопасности для объектов, созданных различными функциями, такими как Createfilea, CreatePipe, CreateProcess, RegCreateKeyEx или RegSaveKeyEx.
        [StructLayout(LayoutKind.Sequential)]
        internal class SECURITY_ATTRIBUTES
        {
            internal int nLength;
            internal unsafe byte* pSecurityDescriptor;
            internal int bInheritHandle;

            public SECURITY_ATTRIBUTES() { }
        }
        //Функция обратного вызова Two Enable-это дополнительная функция обратного вызова, поставляемая драйвером, которая используется для получения уведомлений о включении или отключении.
        internal unsafe delegate void EtwEnableCallback(
          [In] ref Guid sourceId,
          [In] int isEnabled,
          [In] byte level,
          [In] long matchAnyKeywords,
          [In] long matchAllKeywords,
          [In] void* filterData,
          [In] void* callbackContext);

        [StructLayout(LayoutKind.Explicit, Size = 40)]
        internal struct PerfCounterSetInfoStruct
        {
            [FieldOffset(0)]
            internal Guid CounterSetGuid;
            [FieldOffset(16)]
            internal Guid ProviderGuid;
            [FieldOffset(32)]
            internal uint NumCounters;
            [FieldOffset(36)]
            internal uint InstanceType;
        }

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        internal struct PerfCounterInfoStruct
        {
            [FieldOffset(0)]
            internal uint CounterId;
            [FieldOffset(4)]
            internal uint CounterType;
            [FieldOffset(8)]
            internal long Attrib;
            [FieldOffset(16)]
            internal uint Size;
            [FieldOffset(20)]
            internal uint DetailLevel;
            [FieldOffset(24)]
            internal uint Scale;
            [FieldOffset(28)]
            internal uint Offset;
        }

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        internal struct PerfCounterSetInstanceStruct
        {
            [FieldOffset(0)]
            internal Guid CounterSetGuid;
            [FieldOffset(16)]
            internal uint dwSize;
            [FieldOffset(20)]
            internal uint InstanceId;
            [FieldOffset(24)]
            internal uint InstanceNameOffset;
            [FieldOffset(28)]
            internal uint InstanceNameSize;
        }

        internal unsafe delegate uint PERFLIBREQUEST([In] uint RequestCode, [In] void* Buffer, [In] uint BufferSize);
        //Определяет значения, указывающие, как возвращать результаты запроса и выполняется ли запрос к каналу или файлу журнала.
        [Flags]
        internal enum EvtQueryFlags
        {
            //Указывает, что запрос направлен против одного или нескольких каналов. Параметр Path функции EvtQuery должен указывать имя канала или значение NULL.
            EvtQueryChannelPath = 1,
            //Указывает, что запрос выполняется к одному или нескольким файлам журнала. Параметр Path функции EvtQuery должен указывать полный путь к файлу журнала или значение NULL.
            EvtQueryFilePath = 2,
            //Указывает, что события в результате запроса упорядочиваются от самых старых до самых новых. Это значение по умолчанию.
            EvtQueryForwardDirection = 256, // 0x00000100
            //Указывает, что события в результате запроса упорядочиваются от самых новых до самых старых.
            EvtQueryReverseDirection = 512, // 0x00000200
            //Указывает, что EvtQuery должен выполнить запрос, даже если часть запроса генерирует ошибку (плохо сформирована). Служба проверяет синтаксис запроса XPath, чтобы определить, правильно ли он сформирован. 
            //Если проверка завершается неудачно, служба анализирует XPath на отдельные выражения. Он создает новый XPath, начиная с самого левого выражения. Служба проверяет выражение, и если оно действительно, служба добавляет следующее выражение в XPath. 
            //Служба повторяет этот процесс до тех пор, пока не найдет выражение, которое не работает. Затем он использует допустимые выражения, которые он нашел, начиная с самого левого выражения в качестве запроса XPath (что означает, что вы не можете получить события, которые вы ожидали). Если ни одна часть XPath не является допустимой, вызов EvtQuery завершается ошибкой.
            EvtQueryTolerateQueryErrors = 4096, // 0x00001000
        }
        //Определяет возможные значения, определяющие время начала подписки на события.
        [Flags]
        internal enum EvtSubscribeFlags
        {
            //Подпишитесь только на будущие события, соответствующие критериям запроса.
            EvtSubscribeToFutureEvents = 1,
            //Подпишитесь на все существующие и будущие события, соответствующие критериям запроса.
            EvtSubscribeStartAtOldestRecord = 2,
            //Подпишитесь на все существующие и будущие события, соответствующие критериям запроса, которые начинаются после события с закладкой. 
            //Если вы включаете флаг EvtSubscribeStrict, функция EvtSubscribe завершается ошибкой, если событие с закладкой не существует. Если вы не включаете флаг EvtSubscribeStrict и событие с закладкой не существует, подписка начинается с события, которое находится после события, ближайшего к событию с закладкой.
            EvtSubscribeStartAfterBookmark = EvtSubscribeStartAtOldestRecord | EvtSubscribeToFutureEvents, // 0x00000003
            //Завершите подписку, даже если часть запроса генерирует ошибку (плохо сформирована). Служба проверяет синтаксис запроса XPath, чтобы определить, правильно ли он сформирован. Если проверка завершается неудачно, служба анализирует XPath на отдельные выражения. 
            //Он создает новый XPath, начиная с самого левого выражения. Служба проверяет выражение, и если оно действительно, служба добавляет следующее выражение в XPath. Служба повторяет этот процесс до тех пор, пока не найдет выражение, которое не работает. Затем он использует допустимые выражения, которые он нашел, начиная с самого левого выражения в качестве запроса XPath (что означает, что вы не можете получить события, которые вы ожидали). Если ни одна часть XPath не является допустимой, вызов EvtSubscribe завершается ошибкой.
            EvtSubscribeTolerateQueryErrors = 4096, // 0x00001000
            //Принудительно вызов EvtSubscribe завершается ошибкой, если указано EvtSubscribeStartAfterBookmark и событие bookmarked не найдено (возвращаемое значение-ERROR_NOT_FOUND). Кроме того, установите этот флаг, если вы хотите получать уведомления в обратном вызове, когда записи событий отсутствуют.
            EvtSubscribeStrict = 65536, // 0x00010000
        }

        internal enum NativeErrorCodes : uint
        {
            ERROR_SUCCESS = 0,
            ERROR_INVALID_PARAMETER = 87, // 0x00000057
            ERROR_INSUFFICIENT_BUFFER = 122, // 0x0000007A
            ERROR_NO_MORE_ITEMS = 259, // 0x00000103
            ERROR_RESOURCE_LANG_NOT_FOUND = 1815, // 0x00000717
            ERROR_EVT_MESSAGE_NOT_FOUND = 15027, // 0x00003AB3
            ERROR_EVT_MESSAGE_ID_NOT_FOUND = 15028, // 0x00003AB4
            ERROR_EVT_UNRESOLVED_VALUE_INSERT = 15029, // 0x00003AB5
            ERROR_EVT_MESSAGE_LOCALE_NOT_FOUND = 15033, // 0x00003AB9
            ERROR_MUI_FILE_NOT_FOUND = 15100, // 0x00003AFC
        }
        //Определяет возможные типы данных элемента данных варианта.
        internal enum EvtVariantType
        {
            //Нулевое содержимое, которое подразумевает, что элемент, содержащий содержимое, не существует.
            EvtVarTypeNull = 0,
            //Строка Юникода с нулевым завершением.
            EvtVarTypeString = 1,
            EvtVarTypeAnsiString = 2,
            EvtVarTypeSByte = 3,
            EvtVarTypeByte = 4,
            EvtVarTypeInt16 = 5,
            EvtVarTypeUInt16 = 6,
            EvtVarTypeInt32 = 7,
            EvtVarTypeUInt32 = 8,
            EvtVarTypeInt64 = 9,
            EvtVarTypeUInt64 = 10, // 0x0000000A
            EvtVarTypeSingle = 11, // 0x0000000B
            EvtVarTypeDouble = 12, // 0x0000000C
            EvtVarTypeBoolean = 13, // 0x0000000D
            EvtVarTypeBinary = 14, // 0x0000000E
            EvtVarTypeGuid = 15, // 0x0000000F
            EvtVarTypeSizeT = 16, // 0x00000010
            EvtVarTypeFileTime = 17, // 0x00000011
            EvtVarTypeSysTime = 18, // 0x00000012
            EvtVarTypeSid = 19, // 0x00000013
            EvtVarTypeHexInt32 = 20, // 0x00000014
            EvtVarTypeHexInt64 = 21, // 0x00000015
            EvtVarTypeEvtHandle = 32, // 0x00000020
            EvtVarTypeEvtXml = 35, // 0x00000023
            EvtVarTypeStringArray = 129, // 0x00000081
            EvtVarTypeUInt32Array = 136, // 0x00000088
        }

        internal enum EvtMasks
        {
            EVT_VARIANT_TYPE_MASK = 127, // 0x0000007F
            EVT_VARIANT_TYPE_ARRAY = 128, // 0x00000080
        }
        //Содержит данные события или значения свойств.
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
        internal struct EvtVariant
        {
            [FieldOffset(0)]
            public uint UInteger;
            [FieldOffset(0)]
            public int Integer;
            [FieldOffset(0)]
            public byte UInt8;
            [FieldOffset(0)]
            public short Short;
            [FieldOffset(0)]
            public ushort UShort;
            [FieldOffset(0)]
            public uint Bool;
            [FieldOffset(0)]
            public byte ByteVal;
            [FieldOffset(0)]
            public byte SByte;
            [FieldOffset(0)]
            public ulong ULong;
            [FieldOffset(0)]
            public long Long;
            [FieldOffset(0)]
            public double Double;
            [FieldOffset(0)]
            public IntPtr StringVal;
            [FieldOffset(0)]
            public IntPtr AnsiString;
            [FieldOffset(0)]
            public IntPtr SidVal;
            [FieldOffset(0)]
            public IntPtr Binary;
            [FieldOffset(0)]
            public IntPtr Reference;
            [FieldOffset(0)]
            public IntPtr Handle;
            [FieldOffset(0)]
            public IntPtr GuidReference;
            [FieldOffset(0)]
            public ulong FileTime;
            [FieldOffset(0)]
            public IntPtr SystemTime;
            [FieldOffset(8)]
            public uint Count;
            [FieldOffset(12)]
            public uint Type;
        }
        //Определяет значения, определяющие информацию запроса для извлечения.
        internal enum EvtEventPropertyId
        {
            //Не поддерживаемый. Идентификатор запроса, который выбрал событие. Тип варианта этого свойства-Evtvartypeuint32.
            EvtEventQueryIDs,
            //Канал или файл журнала, из которого пришло событие. Тип варианта этого свойства-EvtVarTypeString.
            EvtEventPath,
            //Это значение перечисления отмечает конец значений перечисления. Он может быть использован для выхода из цикла при получении всех свойств.
            EvtEventPropertyIdEND
        }

        internal enum EvtQueryPropertyId
        {
            EvtQueryNames,
            EvtQueryStatuses,
        }
        //Определяет идентификаторы, определяющие свойства метаданных поставщика.
        internal enum EvtPublisherMetadataPropertyId
        {
            EvtPublisherMetadataPublisherGuid,
            EvtPublisherMetadataResourceFilePath,
            EvtPublisherMetadataParameterFilePath,
            EvtPublisherMetadataMessageFilePath,
            EvtPublisherMetadataHelpLink,
            EvtPublisherMetadataPublisherMessageID,
            EvtPublisherMetadataChannelReferences,
            EvtPublisherMetadataChannelReferencePath,
            EvtPublisherMetadataChannelReferenceIndex,
            EvtPublisherMetadataChannelReferenceID,
            EvtPublisherMetadataChannelReferenceFlags,
            EvtPublisherMetadataChannelReferenceMessageID,
            EvtPublisherMetadataLevels,
            EvtPublisherMetadataLevelName,
            EvtPublisherMetadataLevelValue,
            EvtPublisherMetadataLevelMessageID,
            EvtPublisherMetadataTasks,
            EvtPublisherMetadataTaskName,
            EvtPublisherMetadataTaskEventGuid,
            EvtPublisherMetadataTaskValue,
            EvtPublisherMetadataTaskMessageID,
            EvtPublisherMetadataOpcodes,
            EvtPublisherMetadataOpcodeName,
            EvtPublisherMetadataOpcodeValue,
            EvtPublisherMetadataOpcodeMessageID,
            EvtPublisherMetadataKeywords,
            EvtPublisherMetadataKeywordName,
            EvtPublisherMetadataKeywordValue,
            EvtPublisherMetadataKeywordMessageID,
        }
        //Определяет значения, определяющие способ ссылки на канал.
        internal enum EvtChannelReferenceFlags
        {
            //Указывает, что канал импортируется
            EvtChannelReferenceImported = 1,
        }

        internal enum EvtEventMetadataPropertyId
        {
            EventMetadataEventID,
            EventMetadataEventVersion,
            EventMetadataEventChannel,
            EventMetadataEventLevel,
            EventMetadataEventOpcode,
            EventMetadataEventTask,
            EventMetadataEventKeyword,
            EventMetadataEventMessageID,
            EventMetadataEventTemplate,
        }
        //Определяет идентификаторы, определяющие свойства конфигурации канала.
        internal enum EvtChannelConfigPropertyId
        {
            EvtChannelConfigEnabled,
            EvtChannelConfigIsolation,
            EvtChannelConfigType,
            EvtChannelConfigOwningPublisher,
            EvtChannelConfigClassicEventlog,
            EvtChannelConfigAccess,
            EvtChannelLoggingConfigRetention,
            EvtChannelLoggingConfigAutoBackup,
            EvtChannelLoggingConfigMaxSize,
            EvtChannelLoggingConfigLogFilePath,
            EvtChannelPublishingConfigLevel,
            EvtChannelPublishingConfigKeywords,
            EvtChannelPublishingConfigControlGuid,
            EvtChannelPublishingConfigBufferSize,
            EvtChannelPublishingConfigMinBuffers,
            EvtChannelPublishingConfigMaxBuffers,
            EvtChannelPublishingConfigLatency,
            EvtChannelPublishingConfigClockType,
            EvtChannelPublishingConfigSidType,
            EvtChannelPublisherList,
            EvtChannelConfigPropertyIdEND,
        }
        //Определяет идентификаторы, определяющие свойства метаданных файла журнала канала или файла журнала.
        internal enum EvtLogPropertyId
        {
            EvtLogCreationTime,
            EvtLogLastAccessTime,
            EvtLogLastWriteTime,
            EvtLogFileSize,
            EvtLogAttributes,
            EvtLogNumberOfLogRecords,
            EvtLogOldestRecordNumber,
            EvtLogFull,
        }
        //Определяет значения, указывающие, происходят ли события из канала или файла журнала.
        internal enum EvtExportLogFlags
        {
            EvtExportLogChannelPath = 1,
            EvtExportLogFilePath = 2,
            EvtExportLogTolerateQueryErrors = 4096, // 0x00001000
        }
        //Определяет значения, определяющие тип информации для доступа из события.
        internal enum EvtRenderContextFlags
        {
            EvtRenderContextValues,
            EvtRenderContextSystem,
            EvtRenderContextUser,
        }
        //Определяет значения, которые определяют, что нужно отобразить.
        internal enum EvtRenderFlags
        {
            EvtRenderEventValues,
            EvtRenderEventXml,
            EvtRenderBookmark,
        }
        //Определяет значения, определяющие строку сообщения из события в формат.
        internal enum EvtFormatMessageFlags
        {
            EvtFormatMessageEvent = 1,
            EvtFormatMessageLevel = 2,
            EvtFormatMessageTask = 3,
            EvtFormatMessageOpcode = 4,
            EvtFormatMessageKeyword = 5,
            EvtFormatMessageChannel = 6,
            EvtFormatMessageProvider = 7,
            EvtFormatMessageId = 8,
            EvtFormatMessageXml = 9,
        }
        //Определяет идентификаторы, определяющие системные свойства события.
        internal enum EvtSystemPropertyId
        {
            EvtSystemProviderName,
            EvtSystemProviderGuid,
            EvtSystemEventID,
            EvtSystemQualifiers,
            EvtSystemLevel,
            EvtSystemTask,
            EvtSystemOpcode,
            EvtSystemKeywords,
            EvtSystemTimeCreated,
            EvtSystemEventRecordId,
            EvtSystemActivityID,
            EvtSystemRelatedActivityID,
            EvtSystemProcessID,
            EvtSystemThreadID,
            EvtSystemChannel,
            EvtSystemComputer,
            EvtSystemUserID,
            EvtSystemVersion,
            EvtSystemPropertyIdEND,
        }
        //Определяет типы методов подключения, которые можно использовать для подключения к удаленному компьютеру.
        internal enum EvtLoginClass
        {
            EvtRpcLogin = 1,
        }
        //Определяет относительное положение в результирующем наборе, из которого выполняется поиск.
        [Flags]
        internal enum EVT_SEEK_FLAGS
        {
            //Выполните поиск указанного смещения от первой записи в результирующем наборе. Смещение должно быть положительным значением.
            EvtSeekRelativeToFirst = 1,
            //Найдите указанное смещение от последней записи в результирующем наборе. Смещение должно быть отрицательным значением.
            EvtSeekRelativeToLast = 2,
            //Выполните поиск указанного смещения от текущей записи в результирующем наборе. Смещение может быть положительным или отрицательным значением.
            EvtSeekRelativeToCurrent = 3,
            //Выполните поиск указанного смещения от записи закладки в результирующем наборе. Смещение может быть положительным или отрицательным значением.
            EvtSeekRelativeToBookmark = 4,
            //Битовая маска, с помощью которой можно определить, какой из следующих флагов установлен:
            //EvtSeekRelativeToFirst, EvtSeekRelativeToLast, EvtSeekRelativeToBookmark
            EvtSeekOriginMask = 7,
            //Принудительно завершите работу функции, если событие не существует.
            EvtSeekStrict = 0x10000
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
        internal struct EvtStringVariant
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            [FieldOffset(0)]
            public string StringVal;
            [FieldOffset(8)]
            public uint Count;
            [FieldOffset(12)]
            public uint Type;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}
