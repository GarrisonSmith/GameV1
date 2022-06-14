using System;
using System.Collections.Generic;
using System.Text;

namespace Fantasy.Logic.Controls
{
    /// <summary>
    /// Enum describing supported inputs from various controllers.
    /// </summary>
    public enum Inputs
    {
        //
        // Summary:
        //     Reserved.
        None = 0,
        //
        // Summary:
        //     BACKSPACE key on keyboard.
        Back = 8,
        //
        // Summary:
        //     TAB key on keyboard.
        Tab = 9,
        //
        // Summary:
        //     ENTER key on keyboard.
        Enter = 13,
        //
        // Summary:
        //     PAUSE key on keyboard.
        Pause = 19,
        //
        // Summary:
        //     CAPS LOCK key on keyboard.
        CapsLock = 20,
        //
        // Summary:
        //     Kana key on Japanese keyboards.
        Kana = 21,
        //
        // Summary:
        //     Kanji key on Japanese keyboards.
        Kanji = 25,
        //
        // Summary:
        //     ESC key on keyboard.
        Escape = 27,
        //
        // Summary:
        //     IME Convert key on keyboard.
        ImeConvert = 28,
        //
        // Summary:
        //     IME NoConvert key on keyboard.
        ImeNoConvert = 29,
        //
        // Summary:
        //     SPACEBAR key on keyboard.
        Space = 32,
        //
        // Summary:
        //     PAGE UP key on keyboard.
        PageUp = 33,
        //
        // Summary:
        //     PAGE DOWN key on keyboard.
        PageDown = 34,
        //
        // Summary:
        //     END key on keyboard.
        End = 35,
        //
        // Summary:
        //     HOME key on keyboard.
        Home = 36,
        //
        // Summary:
        //     LEFT ARROW key on keyboard.
        Left = 37,
        //
        // Summary:
        //     UP ARROW key on keyboard.
        Up = 38,
        //
        // Summary:
        //     RIGHT ARROW key on keyboard.
        Right = 39,
        //
        // Summary:
        //     DOWN ARROW key on keyboard.
        Down = 40,
        //
        // Summary:
        //     SELECT key on keyboard.
        Select = 41,
        //
        // Summary:
        //     PRINT key on keyboard.
        Print = 42,
        //
        // Summary:
        //     EXECUTE key on keyboard.
        Execute = 43,
        //
        // Summary:
        //     PRINT SCREEN key on keyboard.
        PrintScreen = 44,
        //
        // Summary:
        //     INS key on keyboard.
        Insert = 45,
        //
        // Summary:
        //     DEL key on keyboard.
        Delete = 46,
        //
        // Summary:
        //     HELP key on keyboard.
        Help = 47,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D0 = 48,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D1 = 49,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D2 = 50,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D3 = 51,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D4 = 52,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D5 = 53,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D6 = 54,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D7 = 55,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D8 = 56,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        D9 = 57,
        //
        // Summary:
        //     A key on keyboard.
        A = 65,
        //
        // Summary:
        //     B key on keyboard.
        B = 66,
        //
        // Summary:
        //     C key on keyboard.
        C = 67,
        //
        // Summary:
        //     D key on keyboard.
        D = 68,
        //
        // Summary:
        //     E key on keyboard.
        E = 69,
        //
        // Summary:
        //     F key on keyboard.
        F = 70,
        //
        // Summary:
        //     G key on keyboard.
        G = 71,
        //
        // Summary:
        //     H key on keyboard.
        H = 72,
        //
        // Summary:
        //     I key on keyboard.
        I = 73,
        //
        // Summary:
        //     J key on keyboard.
        J = 74,
        //
        // Summary:
        //     K key on keyboard.
        K = 75,
        //
        // Summary:
        //     L key on keyboard.
        L = 76,
        //
        // Summary:
        //     M key on keyboard.
        M = 77,
        //
        // Summary:
        //     N key on keyboard.
        N = 78,
        //
        // Summary:
        //     O key on keyboard.
        O = 79,
        //
        // Summary:
        //     P key on keyboard.
        P = 80,
        //
        // Summary:
        //     Q key on keyboard.
        Q = 81,
        //
        // Summary:
        //     R key on keyboard.
        R = 82,
        //
        // Summary:
        //     S key on keyboard.
        S = 83,
        //
        // Summary:
        //     T key on keyboard.
        T = 84,
        //
        // Summary:
        //     U key on keyboard.
        U = 85,
        //
        // Summary:
        //     V key on keyboard.
        V = 86,
        //
        // Summary:
        //     W key on keyboard.
        W = 87,
        //
        // Summary:
        //     X key on keyboard.
        X = 88,
        //
        // Summary:
        //     Y key on keyboard.
        Y = 89,
        //
        // Summary:
        //     Z key on keyboard.
        Z = 90,
        //
        // Summary:
        //     Left Windows key on keyboard.
        LeftWindows = 91,
        //
        // Summary:
        //     Right Windows key on keyboard.
        RightWindows = 92,
        //
        // Summary:
        //     Applications key on keyboard.
        Apps = 93,
        //
        // Summary:
        //     Computer Sleep key on keyboard.
        Sleep = 95,
        //
        // Summary:
        //     Numeric keypad 0 key on keyboard.
        NumPad0 = 96,
        //
        // Summary:
        //     Numeric keypad 1 key on keyboard.
        NumPad1 = 97,
        //
        // Summary:
        //     Numeric keypad 2 key on keyboard.
        NumPad2 = 98,
        //
        // Summary:
        //     Numeric keypad 3 key on keyboard.
        NumPad3 = 99,
        //
        // Summary:
        //     Numeric keypad 4 key on keyboard.
        NumPad4 = 100,
        //
        // Summary:
        //     Numeric keypad 5 key on keyboard.
        NumPad5 = 101,
        //
        // Summary:
        //     Numeric keypad 6 key on keyboard.
        NumPad6 = 102,
        //
        // Summary:
        //     Numeric keypad 7 key on keyboard.
        NumPad7 = 103,
        //
        // Summary:
        //     Numeric keypad 8 key on keyboard.
        NumPad8 = 104,
        //
        // Summary:
        //     Numeric keypad 9 key on keyboard.
        NumPad9 = 105,
        //
        // Summary:
        //     Multiply key on keyboard.
        Multiply = 106,
        //
        // Summary:
        //     Add key on keyboard.
        Add = 107,
        //
        // Summary:
        //     Separator key on keyboard.
        Separator = 108,
        //
        // Summary:
        //     Subtract key on keyboard.
        Subtract = 109,
        //
        // Summary:
        //     Decimal key on keyboard.
        Decimal = 110,
        //
        // Summary:
        //     Divide key on keyboard.
        Divide = 111,
        //
        // Summary:
        //     F1 key on keyboard.
        F1 = 112,
        //
        // Summary:
        //     F2 key on keyboard.
        F2 = 113,
        //
        // Summary:
        //     F3 key on keyboard.
        F3 = 114,
        //
        // Summary:
        //     F4 key on keyboard.
        F4 = 115,
        //
        // Summary:
        //     F5 key on keyboard.
        F5 = 116,
        //
        // Summary:
        //     F6 key on keyboard.
        F6 = 117,
        //
        // Summary:
        //     F7 key on keyboard.
        F7 = 118,
        //
        // Summary:
        //     F8 key on keyboard.
        F8 = 119,
        //
        // Summary:
        //     F9 key on keyboard.
        F9 = 120,
        //
        // Summary:
        //     F10 key on keyboard.
        F10 = 121,
        //
        // Summary:
        //     F11 key on keyboard.
        F11 = 122,
        //
        // Summary:
        //     F12 key on keyboard.
        F12 = 123,
        //
        // Summary:
        //     F13 key on keyboard.
        F13 = 124,
        //
        // Summary:
        //     F14 key on keyboard.
        F14 = 125,
        //
        // Summary:
        //     F15 key on keyboard.
        F15 = 126,
        //
        // Summary:
        //     F16 key on keyboard.
        F16 = 127,
        //
        // Summary:
        //     F17 key on keyboard.
        F17 = 128,
        //
        // Summary:
        //     F18 key on keyboard.
        F18 = 129,
        //
        // Summary:
        //     F19 key on keyboard.
        F19 = 130,
        //
        // Summary:
        //     F20 key on keyboard.
        F20 = 131,
        //
        // Summary:
        //     F21 key on keyboard.
        F21 = 132,
        //
        // Summary:
        //     F22 key on keyboard.
        F22 = 133,
        //
        // Summary:
        //     F23 key on keyboard.
        F23 = 134,
        //
        // Summary:
        //     F24 key on keyboard.
        F24 = 135,
        //
        // Summary:
        //     NUM LOCK key on keyboard.
        NumLock = 144,
        //
        // Summary:
        //     SCROLL LOCK key on keyboard.
        Scroll = 145,
        //
        // Summary:
        //     Left SHIFT key on keyboard.
        LeftShift = 160,
        //
        // Summary:
        //     Right SHIFT key on keyboard.
        RightShift = 161,
        //
        // Summary:
        //     Left CONTROL key on keyboard.
        LeftControl = 162,
        //
        // Summary:
        //     Right CONTROL key on keyboard.
        RightControl = 163,
        //
        // Summary:
        //     Left ALT key on keyboard.
        LeftAlt = 164,
        //
        // Summary:
        //     Right ALT key on keyboard.
        RightAlt = 165,
        //
        // Summary:
        //     Browser Back key on keyboard.
        BrowserBack = 166,
        //
        // Summary:
        //     Browser Forward key on keyboard.
        BrowserForward = 167,
        //
        // Summary:
        //     Browser Refresh key on keyboard.
        BrowserRefresh = 168,
        //
        // Summary:
        //     Browser Stop key on keyboard.
        BrowserStop = 169,
        //
        // Summary:
        //     Browser Search key on keyboard.
        BrowserSearch = 170,
        //
        // Summary:
        //     Browser Favorites key on keyboard.
        BrowserFavorites = 171,
        //
        // Summary:
        //     Browser Start and Home key on keyboard.
        BrowserHome = 172,
        //
        // Summary:
        //     Volume Mute key on keyboard.
        VolumeMute = 173,
        //
        // Summary:
        //     Volume Down key on keyboard.
        VolumeDown = 174,
        //
        // Summary:
        //     Volume Up key on keyboard.
        VolumeUp = 175,
        //
        // Summary:
        //     Next Track key on keyboard.
        MediaNextTrack = 176,
        //
        // Summary:
        //     Previous Track key on keyboard.
        MediaPreviousTrack = 177,
        //
        // Summary:
        //     Stop Media key on keyboard.
        MediaStop = 178,
        //
        // Summary:
        //     Play/Pause Media key on keyboard.
        MediaPlayPause = 179,
        //
        // Summary:
        //     Start Mail key on keyboard.
        LaunchMail = 180,
        //
        // Summary:
        //     Select Media key on keyboard.
        SelectMedia = 181,
        //
        // Summary:
        //     Start Application 1 key on keyboard.
        LaunchApplication1 = 182,
        //
        // Summary:
        //     Start Application 2 key on keyboard.
        LaunchApplication2 = 183,
        //
        // Summary:
        //     The OEM Semicolon key on a US standard keyboard.
        OemSemicolon = 186,
        //
        // Summary:
        //     For any country/region, the '+' key on keyboard.
        OemPlus = 187,
        //
        // Summary:
        //     For any country/region, the ',' key on keyboard.
        OemComma = 188,
        //
        // Summary:
        //     For any country/region, the '-' key on keyboard.
        OemMinus = 189,
        //
        // Summary:
        //     For any country/region, the '.' key on keyboard.
        OemPeriod = 190,
        //
        // Summary:
        //     The OEM question mark key on a US standard keyboard.
        OemQuestion = 191,
        //
        // Summary:
        //     The OEM tilde key on a US standard keyboard.
        OemTilde = 192,
        //
        // Summary:
        //     Green ChatPad key on keyboard.
        ChatPadGreen = 202,
        //
        // Summary:
        //     Orange ChatPad key on keyboard.
        ChatPadOrange = 203,
        //
        // Summary:
        //     The OEM open bracket key on a US standard keyboard.
        OemOpenBrackets = 219,
        //
        // Summary:
        //     The OEM pipe key on a US standard keyboard.
        OemPipe = 220,
        //
        // Summary:
        //     The OEM close bracket key on a US standard keyboard.
        OemCloseBrackets = 221,
        //
        // Summary:
        //     The OEM singled/double quote key on a US standard keyboard.
        OemQuotes = 222,
        //
        // Summary:
        //     Used for miscellaneous characters; it can vary by keyboard.
        Oem8 = 223,
        //
        // Summary:
        //     The OEM angle bracket or backslash key on the RT 102 key keyboard.
        OemBackslash = 226,
        //
        // Summary:
        //     IME PROCESS key on keyboard.
        ProcessKey = 229,
        //
        // Summary:
        //     OEM Copy key on keyboard.
        OemCopy = 242,
        //
        // Summary:
        //     OEM Auto key on keyboard.
        OemAuto = 243,
        //
        // Summary:
        //     OEM Enlarge Window key on keyboard.
        OemEnlW = 244,
        //
        // Summary:
        //     Attn key on keyboard.
        Attn = 246,
        //
        // Summary:
        //     CrSel key on keyboard.
        Crsel = 247,
        //
        // Summary:
        //     ExSel key on keyboard.
        Exsel = 248,
        //
        // Summary:
        //     Erase EOF key on keyboard.
        EraseEof = 249,
        //
        // Summary:
        //     Play key on keyboard.
        Play = 250,
        //
        // Summary:
        //     Zoom key on keyboard.
        Zoom = 251,
        //
        // Summary:
        //     PA1 key on keyboard.
        Pa1 = 253,
        //
        // Summary:
        //     CLEAR key on keyboard.
        OemClear = 254,
        //
        // Summary:
        //     Left Click on Mouse.
        LeftButton = 301,
        //
        // Summary:
        //     Right Click on Mouse.
        RightButton = 302,
        //
        // Summary:
        //     Middle Click on Mouse.
        MiddleButton = 303,
        //
        // Summary:
        //     Side Click 1 on Mouse.
        XButton1 = 304,
        //
        // Summary:
        //     Side Click 2 on Mouse..
        XButton2 = 305,
        //
        // Summary:
        //     Side Click 2 on Mouse..
        ScrollIn = 306,
        //
        // Summary:
        //     Side Click 2 on Mouse..
        ScrollOut = 307
    }
}
