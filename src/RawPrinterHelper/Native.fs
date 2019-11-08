namespace RawPrinterHelper

module private Native =
    open System.Runtime.InteropServices

    [<StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
    type DOCINFOA =
        struct
            val mutable pDocName : string
            val mutable pOutputFile : string
            val mutable pDataType : string 
        end

    [<Literal>]
    let DllName = "winspool.Drv"
 
    [<DllImport(DllName, EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, 
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)>]
    extern bool OpenPrinter([<MarshalAs(UnmanagedType.LPStr)>] string szPrinter, [<Out>] nativeint hPrinter, nativeint pd)

    [<DllImport(DllName, EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool ClosePrinter(nativeint hPrinter)

    [<DllImport(DllName, EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi,
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)>]
    extern bool StartDocPrinter(nativeint hPrinter, int level,
            [<In>] [<MarshalAs(UnmanagedType.LPStruct)>] DOCINFOA di)

    [<DllImport(DllName, EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool EndDocPrinter(nativeint hPrinter)

    [<DllImport(DllName, EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool StartPagePrinter(nativeint hPrinter)

    [<DllImport(DllName, EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool EndPagePrinter(nativeint hPrinter)

    [<DllImport(DllName, EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)>]
    extern bool WritePrinter(nativeint hPrinter, nativeint pBytes, int dwCount, [<Out>] int dwWritten)

    [<DllImport(DllName, EntryPoint = "FlushPrinter", SetLastError = true, CharSet = CharSet.Ansi, 
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)>]
    extern bool FlushPrinter(nativeint hPrinter, nativeint pBuf, int32 cbBuf, [<Out>] int32 pcWritten, [<Out>] int32 cSleep)

    [<DllImport(DllName, EntryPoint = "SetJobA", SetLastError = true)>]
    extern int SetJob(nativeint hPrinter, uint32 JobId, uint32 Level, nativeint pJob, uint32 Command_Renamed)
 
    






