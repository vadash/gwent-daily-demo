using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

namespace gwent_daily_reborn.Model.Helpers.Keyboard
{
    /// <summary>Class for messaging and key presses</summary>
    [Serializable]
    internal class Messaging
    {
        #region Unmanaged Items

        #region Constants

        /// <summary>Maps a virtual key to a key code.</summary>
        private const uint MAPVK_VK_TO_VSC = 0x00;

        /// <summary>Maps a key code to a virtual key.</summary>
        private const uint MAPVK_VSC_TO_VK = 0x01;

        /// <summary>Maps a virtual key to a character.</summary>
        private const uint MAPVK_VK_TO_CHAR = 0x02;

        /// <summary>Maps a key code to a virtual key with specified keyboard.</summary>
        private const uint MAPVK_VSC_TO_VK_EX = 0x03;

        /// <summary>Maps a virtual key to a key code with specified keyboard.</summary>
        private const uint MAPVK_VK_TO_VSC_EX = 0x04;

        /// <summary>Code if the key is toggled.</summary>
        private const ushort KEY_TOGGLED = 0x1;

        /// <summary>Code for if the key is pressed.</summary>
        private const ushort KEY_PRESSED = 0xF000;

        /// <summary>Code for no keyboard event.</summary>
        private const uint KEYEVENTF_NONE = 0x0;

        /// <summary>Code for extended key pressed.</summary>
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        /// <summary>Code for keyup event.</summary>
        private const uint KEYEVENTF_KEYUP = 0x0002;

        /// <summary>Mouse input type.</summary>
        private const int INPUT_MOUSE = 0;

        /// <summary>Keyboard input type.</summary>
        private const int INPUT_KEYBOARD = 1;

        /// <summary>Hardware input type.</summary>
        private const int INPUT_HARDWARE = 2;

        #endregion Constants

        [StructLayout(LayoutKind.Explicit)]
        private struct Helper
        {
            [FieldOffset(0)] public short Value;
            [FieldOffset(0)] public readonly byte Low;
            [FieldOffset(1)] public readonly byte High;
        }

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetMessageExtraInfo();

        /// <summary>Gets the key state of the specified key.</summary>
        /// <param name="nVirtKey">The key to check.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern ushort GetKeyState(int nVirtKey);

        /// <summary>Gets the state of the entire keyboard.</summary>
        /// <param name="lpKeyState">The byte array to receive all the keys states.</param>
        /// <returns>Whether it succeed or failed.</returns>
        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>Allows for foreground hardware keyboard key presses</summary>
        /// <param name="nInputs">The number of inputs in pInputs</param>
        /// <param name="pInputs">A Input structure for what is to be pressed.</param>
        /// <param name="cbSize">The size of the structure.</param>
        /// <returns>A message.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        /// <summary>
        ///     The GetForegroundWindow function returns a handle to the foreground window.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, uint wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        #endregion //Unmanaged Items

        #region Structures

        #region Public

        internal enum WindowsMessages
        {
            WM_NULL = 0x00,
            WM_CREATE = 0x01,
            WM_DESTROY = 0x02,
            WM_MOVE = 0x03,
            WM_SIZE = 0x05,
            WM_ACTIVATE = 0x06,
            WM_SETFOCUS = 0x07,
            WM_KILLFOCUS = 0x08,
            WM_ENABLE = 0x0A,
            WM_SETREDRAW = 0x0B,
            WM_SETTEXT = 0x0C,
            WM_GETTEXT = 0x0D,
            WM_GETTEXTLENGTH = 0x0E,
            WM_PAINT = 0x0F,
            WM_CLOSE = 0x10,
            WM_QUERYENDSESSION = 0x11,
            WM_QUIT = 0x12,
            WM_QUERYOPEN = 0x13,
            WM_ERASEBKGND = 0x14,
            WM_SYSCOLORCHANGE = 0x15,
            WM_ENDSESSION = 0x16,
            WM_SYSTEMERROR = 0x17,
            WM_SHOWWINDOW = 0x18,
            WM_CTLCOLOR = 0x19,
            WM_WININICHANGE = 0x1A,
            WM_SETTINGCHANGE = 0x1A,
            WM_DEVMODECHANGE = 0x1B,
            WM_ACTIVATEAPP = 0x1C,
            WM_FONTCHANGE = 0x1D,
            WM_TIMECHANGE = 0x1E,
            WM_CANCELMODE = 0x1F,
            WM_SETCURSOR = 0x20,
            WM_MOUSEACTIVATE = 0x21,
            WM_CHILDACTIVATE = 0x22,
            WM_QUEUESYNC = 0x23,
            WM_GETMINMAXINFO = 0x24,
            WM_PAINTICON = 0x26,
            WM_ICONERASEBKGND = 0x27,
            WM_NEXTDLGCTL = 0x28,
            WM_SPOOLERSTATUS = 0x2A,
            WM_DRAWITEM = 0x2B,
            WM_MEASUREITEM = 0x2C,
            WM_DELETEITEM = 0x2D,
            WM_VKEYTOITEM = 0x2E,
            WM_CHARTOITEM = 0x2F,

            WM_SETFONT = 0x30,
            WM_GETFONT = 0x31,
            WM_SETHOTKEY = 0x32,
            WM_GETHOTKEY = 0x33,
            WM_QUERYDRAGICON = 0x37,
            WM_COMPAREITEM = 0x39,
            WM_COMPACTING = 0x41,
            WM_WINDOWPOSCHANGING = 0x46,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_POWER = 0x48,
            WM_COPYDATA = 0x4A,
            WM_CANCELJOURNAL = 0x4B,
            WM_NOTIFY = 0x4E,
            WM_INPUTLANGCHANGEREQUEST = 0x50,
            WM_INPUTLANGCHANGE = 0x51,
            WM_TCARD = 0x52,
            WM_HELP = 0x53,
            WM_USERCHANGED = 0x54,
            WM_NOTIFYFORMAT = 0x55,
            WM_CONTEXTMENU = 0x7B,
            WM_STYLECHANGING = 0x7C,
            WM_STYLECHANGED = 0x7D,
            WM_DISPLAYCHANGE = 0x7E,
            WM_GETICON = 0x7F,
            WM_SETICON = 0x80,

            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 0x82,
            WM_NCCALCSIZE = 0x83,
            WM_NCHITTEST = 0x84,
            WM_NCPAINT = 0x85,
            WM_NCACTIVATE = 0x86,
            WM_GETDLGCODE = 0x87,
            WM_NCMOUSEMOVE = 0xA0,
            WM_NCLBUTTONDOWN = 0xA1,
            WM_NCLBUTTONUP = 0xA2,
            WM_NCLBUTTONDBLCLK = 0xA3,
            WM_NCRBUTTONDOWN = 0xA4,
            WM_NCRBUTTONUP = 0xA5,
            WM_NCRBUTTONDBLCLK = 0xA6,
            WM_NCMBUTTONDOWN = 0xA7,
            WM_NCMBUTTONUP = 0xA8,
            WM_NCMBUTTONDBLCLK = 0xA9,

            WM_INPUT = 0x00FF,

            WM_KEYFIRST = 0x100,
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_CHAR = 0x102,
            WM_DEADCHAR = 0x103,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_SYSCHAR = 0x106,
            WM_SYSDEADCHAR = 0x107,
            WM_KEYLAST = 0x108,

            WM_IME_STARTCOMPOSITION = 0x10D,
            WM_IME_ENDCOMPOSITION = 0x10E,
            WM_IME_COMPOSITION = 0x10F,
            WM_IME_KEYLAST = 0x10F,

            WM_INITDIALOG = 0x110,
            WM_COMMAND = 0x111,
            WM_SYSCOMMAND = 0x112,
            WM_TIMER = 0x113,
            WM_HSCROLL = 0x114,
            WM_VSCROLL = 0x115,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_MENUSELECT = 0x11F,
            WM_MENUCHAR = 0x120,
            WM_ENTERIDLE = 0x121,

            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 0x136,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,

            WM_MOUSEFIRST = 0x200,
            WM_MOUSEMOVE = 0x200,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_LBUTTONDBLCLK = 0x203,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RBUTTONDBLCLK = 0x206,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MOUSEWHEEL = 0x20A,
            WM_MOUSEHWHEEL = 0x20E,

            WM_PARENTNOTIFY = 0x210,
            WM_ENTERMENULOOP = 0x211,
            WM_EXITMENULOOP = 0x212,
            WM_NEXTMENU = 0x213,
            WM_SIZING = 0x214,
            WM_CAPTURECHANGED = 0x215,
            WM_MOVING = 0x216,
            WM_POWERBROADCAST = 0x218,
            WM_DEVICECHANGE = 0x219,

            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIACTIVATE = 0x222,
            WM_MDIRESTORE = 0x223,
            WM_MDINEXT = 0x224,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDITILE = 0x226,
            WM_MDICASCADE = 0x227,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIGETACTIVE = 0x229,
            WM_MDISETMENU = 0x230,
            WM_ENTERSIZEMOVE = 0x231,
            WM_EXITSIZEMOVE = 0x232,
            WM_DROPFILES = 0x233,
            WM_MDIREFRESHMENU = 0x234,

            WM_IME_SETCONTEXT = 0x281,
            WM_IME_NOTIFY = 0x282,
            WM_IME_CONTROL = 0x283,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_SELECT = 0x285,
            WM_IME_CHAR = 0x286,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYUP = 0x291,

            WM_MOUSEHOVER = 0x2A1,
            WM_NCMOUSELEAVE = 0x2A2,
            WM_MOUSELEAVE = 0x2A3,

            WM_CUT = 0x300,
            WM_COPY = 0x301,
            WM_PASTE = 0x302,
            WM_CLEAR = 0x303,
            WM_UNDO = 0x304,

            WM_RENDERFORMAT = 0x305,
            WM_RENDERALLFORMATS = 0x306,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DRAWCLIPBOARD = 0x308,
            WM_PAINTCLIPBOARD = 0x309,
            WM_VSCROLLCLIPBOARD = 0x30A,
            WM_SIZECLIPBOARD = 0x30B,
            WM_ASKCBFORMATNAME = 0x30C,
            WM_CHANGECBCHAIN = 0x30D,
            WM_HSCROLLCLIPBOARD = 0x30E,
            WM_QUERYNEWPALETTE = 0x30F,
            WM_PALETTEISCHANGING = 0x310,
            WM_PALETTECHANGED = 0x311,

            WM_HOTKEY = 0x312,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,

            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35F,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38F,
            WM_COALESCE_FIRST = 0x390,
            WM_COALESCE_LAST = 0x39F,
            WM_DDE_FIRST = 0x3E0,
            WM_DDE_INITIATE = 0x3E0,
            WM_DDE_TERMINATE = 0x3E1,
            WM_DDE_ADVISE = 0x3E2,
            WM_DDE_UNADVISE = 0x3E3,
            WM_DDE_ACK = 0x3E4,
            WM_DDE_DATA = 0x3E5,
            WM_DDE_REQUEST = 0x3E6,
            WM_DDE_POKE = 0x3E7,
            WM_DDE_EXECUTE = 0x3E8,
            WM_DDE_LAST = 0x3E8,

            WM_USER = 0x400,
            WM_APP = 0x8000
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public readonly int dx;
            public readonly int dy;
            public readonly uint mouseData;
            public readonly uint dwFlags;
            public readonly uint time;
            public readonly IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            /*Virtual Key code.  Must be from 1-254.  If the dwFlags member specifies KEYEVENTF_UNICODE, wVk must be 0.*/
            public ushort wVk;

            /*A hardware scan code for the key. If dwFlags specifies KEYEVENTF_UNICODE, wScan specifies a Unicode character which is to be sent to the foreground application.*/
            public ushort wScan;

            /*Specifies various aspects of a keystroke.  See the KEYEVENTF_ constants for more information.*/
            public uint dwFlags;

            /*The time stamp for the event, in milliseconds. If this parameter is zero, the system will provide its own time stamp.*/
            public uint time;

            /*An additional value associated with the keystroke. Use the GetMessageExtraInfo function to obtain this information.*/
            public readonly IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public readonly uint uMsg;
            public readonly ushort wParamL;
            public readonly ushort wParamH;
        }

        private struct INPUT
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public readonly MOUSEINPUT mi;
            [FieldOffset(0)] public KEYBDINPUT ki;
            [FieldOffset(0)] public readonly HARDWAREINPUT hi;
        }

        [Serializable]
        public enum ShiftType
        {
            NONE = 0x0,
            SHIFT = 0x1,
            CTRL = 0x2,
            SHIFT_CTRL = SHIFT | CTRL,
            ALT = 0x4,
            SHIFT_ALT = ALT | SHIFT,
            CTRL_ALT = CTRL | ALT,
            SHIFT_CTRL_ALT = SHIFT | CTRL | ALT
        }

        public enum Message
        {
            NCHITTEST = 0x0084,
            KEY_DOWN = 0x0100, //Key down
            KEY_UP = 0x0101, //Key Up
            VM_CHAR = 0x0102, //The character being pressed
            SYSKEYDOWN = 0x0104, //An Alt/ctrl/shift + key down message
            SYSKEYUP = 0x0105, //An Alt/Ctrl/Shift + Key up Message
            SYSCHAR = 0x0106, //An Alt/Ctrl/Shift + Key character Message
            LBUTTONDOWN = 0x201, //Left mousebutton down 
            LBUTTONUP = 0x202, //Left mousebutton up 
            LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick 
            RBUTTONDOWN = 0x204, //Right mousebutton down 
            RBUTTONUP = 0x205, //Right mousebutton up 
            RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick

            /// <summary>Middle mouse button down</summary>
            MBUTTONDOWN = 0x207,

            /// <summary>Middle mouse button up</summary>
            MBUTTONUP = 0x208
        }

        [Serializable]
        [ComVisible(true)]
        [SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags", Justification = "Certain members of Keys enum are actually meant to be OR'ed.")]
        internal enum VKeys
        {
            /// <devdoc>
            ///     The bit mask to extract a key code from a key value.
            /// </devdoc>
            KeyCode = 0x0000FFFF,

            /// <devdoc>
            ///     The bit mask to extract modifiers from a key value.
            /// </devdoc>
            Modifiers = unchecked((int) 0xFFFF0000),

            /// <devdoc>
            ///     No key pressed.
            /// </devdoc>
            None = 0x00,

            /// <devdoc>
            ///     The left mouse button.
            /// </devdoc>
            LButton = 0x01,

            /// <devdoc>
            ///     The right mouse button.
            /// </devdoc>
            RButton = 0x02,

            /// <devdoc>
            ///     The CANCEL key.
            /// </devdoc>
            Cancel = 0x03,

            /// <devdoc>
            ///     The middle mouse button (three-button mouse).
            /// </devdoc>
            MButton = 0x04,

            /// <devdoc>
            ///     The first x mouse button (five-button mouse).
            /// </devdoc>
            XButton1 = 0x05,

            /// <devdoc>
            ///     The second x mouse button (five-button mouse).
            /// </devdoc>
            XButton2 = 0x06,

            /// <devdoc>
            ///     <para>
            ///         The BACKSPACE key.
            ///     </para>
            /// </devdoc>
            Back = 0x08,

            /// <devdoc>
            ///     The TAB key.
            /// </devdoc>
            Tab = 0x09,

            /// <devdoc>
            ///     The CLEAR key.
            /// </devdoc>
            LineFeed = 0x0A,

            /// The CLEAR key.
            /// </devdoc>
            Clear = 0x0C,

            /// <devdoc>
            ///     The RETURN key.
            /// </devdoc>
            Return = 0x0D,

            /// <devdoc>
            ///     The ENTER key.
            /// </devdoc>
            Enter = Return,

            /// <devdoc>
            ///     The SHIFT key.
            /// </devdoc>
            ShiftKey = 0x10,

            /// <devdoc>
            ///     The CTRL key.
            /// </devdoc>
            ControlKey = 0x11,

            /// <devdoc>
            ///     The ALT key.
            /// </devdoc>
            Menu = 0x12,

            /// <devdoc>
            ///     The PAUSE key.
            /// </devdoc>
            Pause = 0x13,

            /// <devdoc>
            ///     The CAPS LOCK key.
            /// </devdoc>
            Capital = 0x14,

            /// <devdoc>
            ///     The CAPS LOCK key.
            /// </devdoc>
            CapsLock = 0x14,

            /// <devdoc>
            ///     The IME Kana mode key.
            /// </devdoc>
            KanaMode = 0x15,

            /// <devdoc>
            ///     The IME Hanguel mode key.
            /// </devdoc>
            HanguelMode = 0x15,

            /// <devdoc>
            ///     The IME Hangul mode key.
            /// </devdoc>
            HangulMode = 0x15,

            /// <devdoc>
            ///     The IME Junja mode key.
            /// </devdoc>
            JunjaMode = 0x17,

            /// <devdoc>
            ///     The IME Final mode key.
            /// </devdoc>
            FinalMode = 0x18,

            /// <devdoc>
            ///     The IME Hanja mode key.
            /// </devdoc>
            HanjaMode = 0x19,

            /// <devdoc>
            ///     The IME Kanji mode key.
            /// </devdoc>
            KanjiMode = 0x19,

            /// <devdoc>
            ///     The ESC key.
            /// </devdoc>
            Escape = 0x1B,

            /// <devdoc>
            ///     The IME Convert key.
            /// </devdoc>
            IMEConvert = 0x1C,

            /// <devdoc>
            ///     The IME NonConvert key.
            /// </devdoc>
            IMENonconvert = 0x1D,

            /// <devdoc>
            ///     The IME Accept key.
            /// </devdoc>
            IMEAccept = 0x1E,

            /// <devdoc>
            ///     The IME Accept key.
            /// </devdoc>
            IMEAceept = IMEAccept,

            /// <devdoc>
            ///     The IME Mode change request.
            /// </devdoc>
            IMEModeChange = 0x1F,

            /// <devdoc>
            ///     The SPACEBAR key.
            /// </devdoc>
            Space = 0x20,

            /// <devdoc>
            ///     The PAGE UP key.
            /// </devdoc>
            Prior = 0x21,

            /// <devdoc>
            ///     The PAGE UP key.
            /// </devdoc>
            PageUp = Prior,

            /// <devdoc>
            ///     The PAGE DOWN key.
            /// </devdoc>
            Next = 0x22,

            /// <devdoc>
            ///     The PAGE DOWN key.
            /// </devdoc>
            PageDown = Next,

            /// <devdoc>
            ///     The END key.
            /// </devdoc>
            End = 0x23,

            /// <devdoc>
            ///     The HOME key.
            /// </devdoc>
            Home = 0x24,

            /// <devdoc>
            ///     The LEFT ARROW key.
            /// </devdoc>
            Left = 0x25,

            /// <devdoc>
            ///     The UP ARROW key.
            /// </devdoc>
            Up = 0x26,

            /// <devdoc>
            ///     The RIGHT ARROW key.
            /// </devdoc>
            Right = 0x27,

            /// <devdoc>
            ///     The DOWN ARROW key.
            /// </devdoc>
            Down = 0x28,

            /// <devdoc>
            ///     The SELECT key.
            /// </devdoc>
            Select = 0x29,

            /// <devdoc>
            ///     The PRINT key.
            /// </devdoc>
            Print = 0x2A,

            /// <devdoc>
            ///     The EXECUTE key.
            /// </devdoc>
            Execute = 0x2B,

            /// <devdoc>
            ///     The PRINT SCREEN key.
            /// </devdoc>
            Snapshot = 0x2C,

            /// <devdoc>
            ///     The PRINT SCREEN key.
            /// </devdoc>
            PrintScreen = Snapshot,

            /// <devdoc>
            ///     The INS key.
            /// </devdoc>
            Insert = 0x2D,

            /// <devdoc>
            ///     The DEL key.
            /// </devdoc>
            Delete = 0x2E,

            /// <devdoc>
            ///     The HELP key.
            /// </devdoc>
            Help = 0x2F,

            /// <devdoc>
            ///     The 0 key.
            /// </devdoc>
            D0 = 0x30, // 0

            /// <devdoc>
            ///     The 1 key.
            /// </devdoc>
            D1 = 0x31, // 1

            /// <devdoc>
            ///     The 2 key.
            /// </devdoc>
            D2 = 0x32, // 2

            /// <devdoc>
            ///     The 3 key.
            /// </devdoc>
            D3 = 0x33, // 3

            /// <devdoc>
            ///     The 4 key.
            /// </devdoc>
            D4 = 0x34, // 4

            /// <devdoc>
            ///     The 5 key.
            /// </devdoc>
            D5 = 0x35, // 5

            /// <devdoc>
            ///     The 6 key.
            /// </devdoc>
            D6 = 0x36, // 6

            /// <devdoc>
            ///     The 7 key.
            /// </devdoc>
            D7 = 0x37, // 7

            /// <devdoc>
            ///     The 8 key.
            /// </devdoc>
            D8 = 0x38, // 8

            /// <devdoc>
            ///     The 9 key.
            /// </devdoc>
            D9 = 0x39, // 9

            /// <devdoc>
            ///     The A key.
            /// </devdoc>
            A = 0x41,

            /// <devdoc>
            ///     The B key.
            /// </devdoc>
            B = 0x42,

            /// <devdoc>
            ///     The C key.
            /// </devdoc>
            C = 0x43,

            /// <devdoc>
            ///     The D key.
            /// </devdoc>
            D = 0x44,

            /// <devdoc>
            ///     The E key.
            /// </devdoc>
            E = 0x45,

            /// <devdoc>
            ///     The F key.
            /// </devdoc>
            F = 0x46,

            /// <devdoc>
            ///     The G key.
            /// </devdoc>
            G = 0x47,

            /// <devdoc>
            ///     The H key.
            /// </devdoc>
            H = 0x48,

            /// <devdoc>
            ///     The I key.
            /// </devdoc>
            I = 0x49,

            /// <devdoc>
            ///     The J key.
            /// </devdoc>
            J = 0x4A,

            /// <devdoc>
            ///     The K key.
            /// </devdoc>
            K = 0x4B,

            /// <devdoc>
            ///     The L key.
            /// </devdoc>
            L = 0x4C,

            /// <devdoc>
            ///     The M key.
            /// </devdoc>
            M = 0x4D,

            /// <devdoc>
            ///     The N key.
            /// </devdoc>
            N = 0x4E,

            /// <devdoc>
            ///     The O key.
            /// </devdoc>
            O = 0x4F,

            /// <devdoc>
            ///     The P key.
            /// </devdoc>
            P = 0x50,

            /// <devdoc>
            ///     The Q key.
            /// </devdoc>
            Q = 0x51,

            /// <devdoc>
            ///     The R key.
            /// </devdoc>
            R = 0x52,

            /// <devdoc>
            ///     The S key.
            /// </devdoc>
            S = 0x53,

            /// <devdoc>
            ///     The T key.
            /// </devdoc>
            T = 0x54,

            /// <devdoc>
            ///     The U key.
            /// </devdoc>
            U = 0x55,

            /// <devdoc>
            ///     The V key.
            /// </devdoc>
            V = 0x56,

            /// <devdoc>
            ///     The W key.
            /// </devdoc>
            W = 0x57,

            /// <devdoc>
            ///     The X key.
            /// </devdoc>
            X = 0x58,

            /// <devdoc>
            ///     The Y key.
            /// </devdoc>
            Y = 0x59,

            /// <devdoc>
            ///     The Z key.
            /// </devdoc>
            Z = 0x5A,

            /// <devdoc>
            ///     The left Windows logo key (Microsoft Natural Keyboard).
            /// </devdoc>
            LWin = 0x5B,

            /// <devdoc>
            ///     The right Windows logo key (Microsoft Natural Keyboard).
            /// </devdoc>
            RWin = 0x5C,

            /// <devdoc>
            ///     The Application key (Microsoft Natural Keyboard).
            /// </devdoc>
            Apps = 0x5D,

            /// <devdoc>
            ///     The Computer Sleep key.
            /// </devdoc>
            Sleep = 0x5F,

            /// <devdoc>
            ///     The 0 key on the numeric keypad.
            /// </devdoc>
            [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")] // PM team has reviewed and decided on naming changes already
            NumPad0 = 0x60,

            /// <devdoc>
            ///     The 1 key on the numeric keypad.
            /// </devdoc>
            [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")] // PM team has reviewed and decided on naming changes already
            NumPad1 = 0x61,

            /// <devdoc>
            ///     The 2 key on the numeric keypad.
            /// </devdoc>
            NumPad2 = 0x62,

            /// <devdoc>
            ///     The 3 key on the numeric keypad.
            /// </devdoc>
            NumPad3 = 0x63,

            /// <devdoc>
            ///     The 4 key on the numeric keypad.
            /// </devdoc>
            NumPad4 = 0x64,

            /// <devdoc>
            ///     The 5 key on the numeric keypad.
            /// </devdoc>
            NumPad5 = 0x65,

            /// <devdoc>
            ///     The 6 key on the numeric keypad.
            /// </devdoc>
            NumPad6 = 0x66,

            /// <devdoc>
            ///     The 7 key on the numeric keypad.
            /// </devdoc>
            NumPad7 = 0x67,

            /// <devdoc>
            ///     The 8 key on the numeric keypad.
            /// </devdoc>
            NumPad8 = 0x68,

            /// <devdoc>
            ///     The 9 key on the numeric keypad.
            /// </devdoc>
            NumPad9 = 0x69,

            /// <devdoc>
            ///     The Multiply key.
            /// </devdoc>
            Multiply = 0x6A,

            /// <devdoc>
            ///     The Add key.
            /// </devdoc>
            Add = 0x6B,

            /// <devdoc>
            ///     The Separator key.
            /// </devdoc>
            Separator = 0x6C,

            /// <devdoc>
            ///     The Subtract key.
            /// </devdoc>
            Subtract = 0x6D,

            /// <devdoc>
            ///     The Decimal key.
            /// </devdoc>
            Decimal = 0x6E,

            /// <devdoc>
            ///     The Divide key.
            /// </devdoc>
            Divide = 0x6F,

            /// <devdoc>
            ///     The F1 key.
            /// </devdoc>
            F1 = 0x70,

            /// <devdoc>
            ///     The F2 key.
            /// </devdoc>
            F2 = 0x71,

            /// <devdoc>
            ///     The F3 key.
            /// </devdoc>
            F3 = 0x72,

            /// <devdoc>
            ///     The F4 key.
            /// </devdoc>
            F4 = 0x73,

            /// <devdoc>
            ///     The F5 key.
            /// </devdoc>
            F5 = 0x74,

            /// <devdoc>
            ///     The F6 key.
            /// </devdoc>
            F6 = 0x75,

            /// <devdoc>
            ///     The F7 key.
            /// </devdoc>
            F7 = 0x76,

            /// <devdoc>
            ///     The F8 key.
            /// </devdoc>
            F8 = 0x77,

            /// <devdoc>
            ///     The F9 key.
            /// </devdoc>
            F9 = 0x78,

            /// <devdoc>
            ///     The F10 key.
            /// </devdoc>
            F10 = 0x79,

            /// <devdoc>
            ///     The F11 key.
            /// </devdoc>
            F11 = 0x7A,

            /// <devdoc>
            ///     The F12 key.
            /// </devdoc>
            F12 = 0x7B,

            /// <devdoc>
            ///     The F13 key.
            /// </devdoc>
            F13 = 0x7C,

            /// <devdoc>
            ///     The F14 key.
            /// </devdoc>
            F14 = 0x7D,

            /// <devdoc>
            ///     The F15 key.
            /// </devdoc>
            F15 = 0x7E,

            /// <devdoc>
            ///     The F16 key.
            /// </devdoc>
            F16 = 0x7F,

            /// <devdoc>
            ///     The F17 key.
            /// </devdoc>
            F17 = 0x80,

            /// <devdoc>
            ///     The F18 key.
            /// </devdoc>
            F18 = 0x81,

            /// <devdoc>
            ///     The F19 key.
            /// </devdoc>
            F19 = 0x82,

            /// <devdoc>
            ///     The F20 key.
            /// </devdoc>
            F20 = 0x83,

            /// <devdoc>
            ///     The F21 key.
            /// </devdoc>
            F21 = 0x84,

            /// <devdoc>
            ///     The F22 key.
            /// </devdoc>
            F22 = 0x85,

            /// <devdoc>
            ///     The F23 key.
            /// </devdoc>
            F23 = 0x86,

            /// <devdoc>
            ///     The F24 key.
            /// </devdoc>
            F24 = 0x87,

            /// <devdoc>
            ///     The NUM LOCK key.
            /// </devdoc>
            [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")] // PM team has reviewed and decided on naming changes already
            NumLock = 0x90,

            /// <devdoc>
            ///     The SCROLL LOCK key.
            /// </devdoc>
            Scroll = 0x91,

            /// <devdoc>
            ///     The left SHIFT key.
            /// </devdoc>
            LShiftKey = 0xA0,

            /// <devdoc>
            ///     The right SHIFT key.
            /// </devdoc>
            RShiftKey = 0xA1,

            /// <devdoc>
            ///     The left CTRL key.
            /// </devdoc>
            LControlKey = 0xA2,

            /// <devdoc>
            ///     The right CTRL key.
            /// </devdoc>
            RControlKey = 0xA3,

            /// <devdoc>
            ///     The left ALT key.
            /// </devdoc>
            LMenu = 0xA4,

            /// <devdoc>
            ///     The right ALT key.
            /// </devdoc>
            RMenu = 0xA5,

            /// <devdoc>
            ///     The Browser Back key.
            /// </devdoc>
            BrowserBack = 0xA6,

            /// <devdoc>
            ///     The Browser Forward key.
            /// </devdoc>
            BrowserForward = 0xA7,

            /// <devdoc>
            ///     The Browser Refresh key.
            /// </devdoc>
            BrowserRefresh = 0xA8,

            /// <devdoc>
            ///     The Browser Stop key.
            /// </devdoc>
            BrowserStop = 0xA9,

            /// <devdoc>
            ///     The Browser Search key.
            /// </devdoc>
            BrowserSearch = 0xAA,

            /// <devdoc>
            ///     The Browser Favorites key.
            /// </devdoc>
            BrowserFavorites = 0xAB,

            /// <devdoc>
            ///     The Browser Home key.
            /// </devdoc>
            BrowserHome = 0xAC,

            /// <devdoc>
            ///     The Volume Mute key.
            /// </devdoc>
            VolumeMute = 0xAD,

            /// <devdoc>
            ///     The Volume Down key.
            /// </devdoc>
            VolumeDown = 0xAE,

            /// <devdoc>
            ///     The Volume Up key.
            /// </devdoc>
            VolumeUp = 0xAF,

            /// <devdoc>
            ///     The Media Next Track key.
            /// </devdoc>
            MediaNextTrack = 0xB0,

            /// <devdoc>
            ///     The Media Previous Track key.
            /// </devdoc>
            MediaPreviousTrack = 0xB1,

            /// <devdoc>
            ///     The Media Stop key.
            /// </devdoc>
            MediaStop = 0xB2,

            /// <devdoc>
            ///     The Media Play Pause key.
            /// </devdoc>
            MediaPlayPause = 0xB3,

            /// <devdoc>
            ///     The Launch Mail key.
            /// </devdoc>
            LaunchMail = 0xB4,

            /// <devdoc>
            ///     The Select Media key.
            /// </devdoc>
            SelectMedia = 0xB5,

            /// <devdoc>
            ///     The Launch Application1 key.
            /// </devdoc>
            LaunchApplication1 = 0xB6,

            /// <devdoc>
            ///     The Launch Application2 key.
            /// </devdoc>
            LaunchApplication2 = 0xB7,

            /// <devdoc>
            ///     The Oem Semicolon key.
            /// </devdoc>
            OemSemicolon = 0xBA,

            /// <devdoc>
            ///     The Oem 1 key.
            /// </devdoc>
            Oem1 = OemSemicolon,

            /// <devdoc>
            ///     The Oem plus key.
            /// </devdoc>
            Oemplus = 0xBB,

            /// <devdoc>
            ///     The Oem comma key.
            /// </devdoc>
            Oemcomma = 0xBC,

            /// <devdoc>
            ///     The Oem Minus key.
            /// </devdoc>
            OemMinus = 0xBD,

            /// <devdoc>
            ///     The Oem Period key.
            /// </devdoc>
            OemPeriod = 0xBE,

            /// <devdoc>
            ///     The Oem Question key.
            /// </devdoc>
            OemQuestion = 0xBF,

            /// <devdoc>
            ///     The Oem 2 key.
            /// </devdoc>
            Oem2 = OemQuestion,

            /// <devdoc>
            ///     The Oem tilde key.
            /// </devdoc>
            Oemtilde = 0xC0,

            /// <devdoc>
            ///     The Oem 3 key.
            /// </devdoc>
            Oem3 = Oemtilde,

            /// <devdoc>
            ///     The Oem Open Brackets key.
            /// </devdoc>
            OemOpenBrackets = 0xDB,

            /// <devdoc>
            ///     The Oem 4 key.
            /// </devdoc>
            Oem4 = OemOpenBrackets,

            /// <devdoc>
            ///     The Oem Pipe key.
            /// </devdoc>
            OemPipe = 0xDC,

            /// <devdoc>
            ///     The Oem 5 key.
            /// </devdoc>
            Oem5 = OemPipe,

            /// <devdoc>
            ///     The Oem Close Brackets key.
            /// </devdoc>
            OemCloseBrackets = 0xDD,

            /// <devdoc>
            ///     The Oem 6 key.
            /// </devdoc>
            Oem6 = OemCloseBrackets,

            /// <devdoc>
            ///     The Oem Quotes key.
            /// </devdoc>
            OemQuotes = 0xDE,

            /// <devdoc>
            ///     The Oem 7 key.
            /// </devdoc>
            Oem7 = OemQuotes,

            /// <devdoc>
            ///     The Oem8 key.
            /// </devdoc>
            Oem8 = 0xDF,

            /// <devdoc>
            ///     The Oem Backslash key.
            /// </devdoc>
            OemBackslash = 0xE2,

            /// <devdoc>
            ///     The Oem 102 key.
            /// </devdoc>
            Oem102 = OemBackslash,

            /// <devdoc>
            ///     The PROCESS KEY key.
            /// </devdoc>
            ProcessKey = 0xE5,

            /// <devdoc>
            ///     The Packet KEY key.
            /// </devdoc>
            Packet = 0xE7,

            /// <devdoc>
            ///     The ATTN key.
            /// </devdoc>
            Attn = 0xF6,

            /// <devdoc>
            ///     The CRSEL key.
            /// </devdoc>
            Crsel = 0xF7,

            /// <devdoc>
            ///     The EXSEL key.
            /// </devdoc>
            Exsel = 0xF8,

            /// <devdoc>
            ///     The ERASE EOF key.
            /// </devdoc>
            EraseEof = 0xF9,

            /// <devdoc>
            ///     The PLAY key.
            /// </devdoc>
            Play = 0xFA,

            /// <devdoc>
            ///     The ZOOM key.
            /// </devdoc>
            Zoom = 0xFB,

            /// <devdoc>
            ///     A constant reserved for future use.
            /// </devdoc>
            NoName = 0xFC,

            /// <devdoc>
            ///     The PA1 key.
            /// </devdoc>
            Pa1 = 0xFD,

            /// <devdoc>
            ///     The CLEAR key.
            /// </devdoc>
            OemClear = 0xFE,

            /// <devdoc>
            ///     The SHIFT modifier key.
            /// </devdoc>
            Shift = 0x00010000,

            /// <devdoc>
            ///     The  CTRL modifier key.
            /// </devdoc>
            Control = 0x00020000,

            /// <devdoc>
            ///     The ALT modifier key.
            /// </devdoc>
            Alt = 0x00040000,

            NULL = 0
        }

        #endregion //Public

        #endregion //Structures

        #region Methods

        #region Public

        public static bool GetKeyState(Key key)
        {
            return (GetKeyState((int) key.Vk) & 0x8000) != 0;
        }

        public static uint GetVirtualKeyCode(char c)
        {
            var helper = new Helper
            {
                Value = VkKeyScan(c)
            };

            var virtualKeyCode = helper.Low;
            var shiftState = helper.High;

            return virtualKeyCode;
        }

        public static void BackgroundMousePosition(IntPtr hWnd, int x, int y)
        {
            PostMessage(hWnd, (int) WindowsMessages.WM_MOUSEMOVE, 0, GetLParam(x, y));
        }

        public static void BackgroundMouseClick(IntPtr hWnd, Key key, int x, int y, int delay = 100)
        {
            switch (key.Vk)
            {
                case VKeys.MButton:
                    PostMessage(hWnd, (int) Message.MBUTTONDOWN, (uint) key.Vk, GetLParam(x, y));
                    Thread.Sleep(delay);
                    PostMessage(hWnd, (int) Message.MBUTTONUP, (uint) key.Vk, GetLParam(x, y));
                    break;
                case VKeys.LButton:
                    PostMessage(hWnd, (int) Message.LBUTTONDOWN, (uint) key.Vk, GetLParam(x, y));
                    Thread.Sleep(delay);
                    PostMessage(hWnd, (int) Message.LBUTTONUP, (uint) key.Vk, GetLParam(x, y));
                    break;
                case VKeys.RButton:
                    PostMessage(hWnd, (int) Message.RBUTTONDOWN, (uint) key.Vk, GetLParam(x, y));
                    Thread.Sleep(delay);
                    PostMessage(hWnd, (int) Message.RBUTTONUP, (uint) key.Vk, GetLParam(x, y));
                    break;
            }
        }

        public static void SendChatTextPost(IntPtr hWnd, string msg)
        {
            PostMessage(hWnd, new Key(VKeys.Enter));
            foreach (var c in msg)
                PostMessage(hWnd, new Key(c));
            PostMessage(hWnd, new Key(VKeys.Enter));
        }

        public static void SendChatTextSend(IntPtr hWnd, string msg)
        {
            SendMessage(hWnd, new Key(VKeys.Enter), true);
            foreach (var c in msg)
                SendChar(hWnd, c, true);
            SendMessage(hWnd, new Key(VKeys.Enter), true);
        }

        public static bool ForegroundKeyPress(Key key, int delay = 100)
        {
            var temp = true;

            temp &= ForegroundKeyDown(key);
            Thread.Sleep(delay);
            temp &= ForegroundKeyUp(key);
            Thread.Sleep(delay);
            return temp;
        }

        public static bool ForegroundKeyPress(IntPtr hWnd, Key key, int delay = 100)
        {
            var temp = true;

            temp &= ForegroundKeyDown(hWnd, key);
            Thread.Sleep(delay);
            temp &= ForegroundKeyUp(hWnd, key);
            Thread.Sleep(delay);
            return temp;
        }

        public static bool ForegroundKeyDown(Key key)
        {
            uint intReturn;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = INPUT_KEYBOARD;

            // Key down shift, ctrl, and/or alt
            structInput.u.ki.wScan = 0;
            structInput.u.ki.time = 0;
            structInput.u.ki.dwFlags = 0;
            // Key down the actual key-code
            structInput.u.ki.wVk = (ushort) key.Vk;
            intReturn = SendInput(1, new[]
            {
                structInput
            }, Marshal.SizeOf(new INPUT()));

            // Key up shift, ctrl, and/or alt
            //keybd_event((int)key.VK, GetScanCode(key.VK) + 0x80, KEYEVENTF_NONE, 0);
            //keybd_event((int)key.VK, GetScanCode(key.VK) + 0x80, KEYEVENTF_KEYUP, 0);
            return true;
        }

        public static bool ForegroundKeyUp(Key key)
        {
            uint intReturn;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = INPUT_KEYBOARD;

            // Key down shift, ctrl, and/or alt
            structInput.u.ki.wScan = 0;
            structInput.u.ki.time = 0;
            structInput.u.ki.dwFlags = 0;
            // Key down the actual key-code
            structInput.u.ki.wVk = (ushort) key.Vk;

            // Key up the actual key-code
            structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
            intReturn = SendInput(1, new[]
            {
                structInput
            }, Marshal.SizeOf(typeof(INPUT)));
            return true;
        }

        public static bool ForegroundKeyDown(IntPtr hWnd, Key key)
        {
            if (GetForegroundWindow() != hWnd)
                if (!SetForegroundWindow(hWnd))
                    return false;
            return ForegroundKeyDown(key);
        }

        public static bool ForegroundKeyUp(IntPtr hWnd, Key key)
        {
            if (GetForegroundWindow() != hWnd)
                if (!SetForegroundWindow(hWnd))
                    return false;
            return ForegroundKeyUp(key);
        }

        public static bool ForegroundKeyPressAll(IntPtr hWnd, Key key, bool alt, bool ctrl, bool shift, int delay = 100)
        {
            if (GetForegroundWindow() != hWnd)
                if (!SetForegroundWindow(hWnd))
                    return false;
            uint intReturn;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = INPUT_KEYBOARD;

            // Key down shift, ctrl, and/or alt
            structInput.u.ki.wScan = 0;
            structInput.u.ki.time = 0;
            structInput.u.ki.dwFlags = 0;
            if (alt)
            {
                structInput.u.ki.wVk = (ushort) VKeys.Menu;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (ctrl)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ControlKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (shift)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ShiftKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);

                if (key.ShiftKey != VKeys.NULL)
                {
                    structInput.u.ki.wVk = (ushort) key.ShiftKey;
                    intReturn = SendInput(1, new[]
                    {
                        structInput
                    }, Marshal.SizeOf(new INPUT()));
                    Thread.Sleep(delay);
                }
            }

            // Key up the actual key-code			
            ForegroundKeyPress(hWnd, key);

            structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
            if (shift && key.ShiftKey == VKeys.NULL)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ShiftKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (ctrl)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ControlKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (alt)
            {
                structInput.u.ki.wVk = (ushort) VKeys.Menu;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            return true;
        }

        public static bool PostMessage(IntPtr hWnd, Key key, int delay = 100)
        {
            //Send KEY_DOWN
            if (PostMessage(hWnd, (int) Message.KEY_DOWN, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);
            //Send VM_CHAR
            if (PostMessage(hWnd, (int) Message.VM_CHAR, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);
            if (PostMessage(hWnd, (int) Message.KEY_UP, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);

            return true;
        }

        public static bool PostMessageAll(IntPtr hWnd, Key key, bool alt, bool ctrl, bool shift, int delay = 100)
        {
            CheckKeyShiftState();
            uint intReturn;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = INPUT_KEYBOARD;

            // Key down shift, ctrl, and/or alt
            structInput.u.ki.wScan = 0;
            structInput.u.ki.time = 0;
            structInput.u.ki.dwFlags = 0;
            if (alt)
            {
                structInput.u.ki.wVk = (ushort) VKeys.Menu;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (ctrl)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ControlKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (shift)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ShiftKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);

                if (key.ShiftKey != VKeys.NULL)
                {
                    //Send KEY_DOWN
                    if (PostMessage(hWnd, (int) Message.KEY_DOWN, (uint) key.Vk, GetLParam(1, key.ShiftKey, 0, 0, 0, 0)))
                        return false;
                    Thread.Sleep(delay);
                }
            }

            PostMessage(hWnd, key);

            structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
            if (shift && key.ShiftKey == VKeys.NULL)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ShiftKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (ctrl)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ControlKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (alt)
            {
                structInput.u.ki.wVk = (ushort) VKeys.Menu;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }

            return true;
        }

        public static bool SendMessageDown(IntPtr hWnd, Key key, bool checkKeyboardState, int delay = 100)
        {
            if (checkKeyboardState)
                CheckKeyShiftState();
            //Send KEY_DOWN
            if (SendMessage(hWnd, (int) Message.KEY_DOWN, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);

            //Send VM_CHAR
            if (SendMessage(hWnd, (int) Message.VM_CHAR, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);

            return true;
        }

        public static bool SendMessageUp(IntPtr hWnd, Key key, bool checkKeyboardState, int delay = 100)
        {
            if (checkKeyboardState)
                CheckKeyShiftState();

            //Send KEY_UP
            if (SendMessage(hWnd, (int) Message.KEY_UP, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 1, 1)))
                return false;
            Thread.Sleep(delay);

            return true;
        }

        public static bool SendChar(IntPtr hWnd, char c, bool checkKeyboardState)
        {
            if (checkKeyboardState)
                CheckKeyShiftState();

            //Send VM_CHAR
            if (SendMessage(hWnd, (int) Message.VM_CHAR, c, 0))
                return false;

            return true;
        }

        public static bool SendMessage(IntPtr hWnd, Key key, bool checkKeyboardState, int delay = 100)
        {
            if (checkKeyboardState)
                CheckKeyShiftState();

            //Send KEY_DOWN
            if (SendMessage(hWnd, (int) Message.KEY_DOWN, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);

            //Send VM_CHAR
            if (SendMessage(hWnd, (int) Message.VM_CHAR, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
                return false;
            Thread.Sleep(delay);

            //Send KEY_UP
            if (SendMessage(hWnd, (int) Message.KEY_UP, (uint) key.Vk, GetLParam(1, key.Vk, 0, 0, 1, 1)))
                return false;
            Thread.Sleep(delay);

            return true;
        }

        public static bool SendMessageAll(IntPtr hWnd, Key key, bool alt, bool ctrl, bool shift, int delay = 100)
        {
            CheckKeyShiftState();
            uint intReturn;
            var structInput = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki =
                    {
                        wScan = 0,
                        time = 0,
                        dwFlags = 0
                    }
                }
            };

            // Key down shift, ctrl, and/or alt
            if (alt)
            {
                structInput.u.ki.wVk = (ushort) VKeys.Menu;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (ctrl)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ControlKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (shift)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ShiftKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);

                if (key.ShiftKey != VKeys.NULL)
                {
                    //Send KEY_DOWN
                    if (SendMessage(hWnd, (int) Message.KEY_DOWN, (uint) key.Vk, GetLParam(1, key.ShiftKey, 0, 0, 0, 0)))
                        return false;
                    Thread.Sleep(delay);
                }
            }

            SendMessage(hWnd, key, false);

            structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
            if (shift && key.ShiftKey == VKeys.NULL)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ShiftKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (ctrl)
            {
                structInput.u.ki.wVk = (ushort) VKeys.ControlKey;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }
            if (alt)
            {
                structInput.u.ki.wVk = (ushort) VKeys.Menu;
                intReturn = SendInput(1, new[]
                {
                    structInput
                }, Marshal.SizeOf(new INPUT()));
                Thread.Sleep(delay);
            }

            return true;
        }

        public static void CheckKeyShiftState()
        {
            while ((GetKeyState((int) VKeys.Menu) & KEY_PRESSED) == KEY_PRESSED ||
                   (GetKeyState((int) VKeys.ControlKey) & KEY_PRESSED) == KEY_PRESSED ||
                   (GetKeyState((int) VKeys.ShiftKey) & KEY_PRESSED) == KEY_PRESSED)
                Thread.Sleep(1);
        }

        #endregion //Public

        #region Private

        private static uint GetScanCode(VKeys key)
        {
            return MapVirtualKey((uint) key, MAPVK_VK_TO_VSC_EX);
        }

        private static uint GetDwExtraInfo(short repeatCount, VKeys key, byte extended, byte contextCode, byte previousState,
            byte transitionState)
        {
            var lParam = (uint) repeatCount;
            var scanCode = MapVirtualKey((uint) key, MAPVK_VK_TO_VSC_EX) + 0x80;
            lParam += scanCode * 0x10000;
            lParam += (uint) (extended * 0x1000000);
            lParam += (uint) (contextCode * 2 * 0x10000000);
            lParam += (uint) (previousState * 4 * 0x10000000);
            lParam += (uint) (transitionState * 8 * 0x10000000);
            return lParam;
        }

        private static uint GetLParam(int x, int y)
        {
            return (uint) ((y << 16) | (x & 0xFFFF));
        }

        private static uint GetLParam(short repeatCount, VKeys key, byte extended, byte contextCode, byte previousState,
            byte transitionState)
        {
            var lParam = (uint) repeatCount;
            //uint scanCode = MapVirtualKey((uint)key, MAPVK_VK_TO_CHAR);
            var scanCode = GetScanCode(key);
            lParam += scanCode * 0x10000;
            lParam += (uint) (extended * 0x1000000);
            lParam += (uint) (contextCode * 2 * 0x10000000);
            lParam += (uint) (previousState * 4 * 0x10000000);
            lParam += (uint) (transitionState * 8 * 0x10000000);
            return lParam;
        }

        private static uint RemoveLeadingDigit(uint number)
        {
            return number - number % 0x10000000 * 0x10000000;
        }

        #endregion Private

        #endregion //Methods
    }
}
