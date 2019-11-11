namespace RawPrinterHelper

module Printer =
    open System
    open System.IO
    open System.Runtime.InteropServices
    open Native

    let randomStr = 
        let chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789"
        let charsLen = chars.Length
        let random = Random()
        fun len -> 
            let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
            String(randomChars)

    let getError =
        Marshal.GetLastWin32Error()  

    let private sendToPrinter szPrinterName pBytes dwCount =
        let mutable dwWritten = 0
        let mutable hPrinter = new nativeint(0)
        let mutable di = DOCINFOA(pDocName = randomStr 8, pDataType = "RAW", pOutputFile = null)
        let mutable bSuccess = false

        if OpenPrinter(szPrinterName, hPrinter, IntPtr.Zero) then
            if StartDocPrinter(hPrinter, 1, di) then
                if StartPagePrinter(hPrinter) then
                    if WritePrinter(hPrinter, pBytes, dwCount, dwWritten) then
                        bSuccess <- true
                    EndPagePrinter(hPrinter) |> ignore                        
                EndDocPrinter(hPrinter) |> ignore 
            ClosePrinter(hPrinter) |> ignore 

        match bSuccess with
        | true -> 0
        | _ ->  getError
 
    
    let SendBytesToPrinter (szPrinterName: string, data: byte[]) =
        let len = Array.length data
        let pUnmanagedBytes = Marshal.AllocCoTaskMem(Array.length data)
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   

        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len
        
        Marshal.FreeCoTaskMem(pUnmanagedBytes)        
        retVal

    let SendStringToPrinter (szPrinterName: string, szString: string) =
        // How many characters are in the string?
        let dwCount = szString.Length
        
        // Assume that the printer is expecting ANSI text, and then convert
        // the string to ANSI text.
        let pBytes = Marshal.StringToCoTaskMemAnsi(szString)

        // Send the converted ANSI string to the printer.
        let retVal = sendToPrinter szPrinterName pBytes dwCount
        retVal
    
    let SendAsciiToPrinter (szPrinterName: string, szString: string) =
        let enc = System.Text.Encoding.ASCII.GetBytes szString
        //if  you are using UTF-8 and get wrong values in qrcode printing, you must use ASCII instead.
        //let enc = System.Text.Encoding.UTF8.GetBytes(data)        
        let retVal = SendBytesToPrinter(szPrinterName, enc)
        retVal

    let SendFileToPrinter (szPrinterName: string, szFileName: string) =
        use stream = File.Open(szFileName, FileMode.Open, FileAccess.Read)
        use mem = new MemoryStream()
        stream.CopyTo mem
        let data = mem.ToArray()
        let len = Array.length data

        // Allocate some unmanaged memory for those bytes.
        let pUnmanagedBytes = Marshal.AllocCoTaskMem(len)
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   
        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len

        Marshal.FreeCoTaskMem(pUnmanagedBytes) 
        retVal
         




        
    

