namespace PrinterCupsHelper

module Printer =
    open System.Diagnostics
    open Utils
        
    let private sendToPrinter szPrinterName fileName =
        let lpCmd = "-c lp -d " + szPrinterName + " " + fileName
        
        let startInfo = 
            ProcessStartInfo(FileName="/bin/bash", 
                Arguments=lpCmd,
                CreateNoWindow=false,
                WindowStyle=ProcessWindowStyle.Normal,
                UseShellExecute=false)

        let proc = new Process(StartInfo = startInfo)
        let started = 
            try
                proc.Start()
                
            with 
            | ex ->
                failwithf "Failed to start process %s | %s" lpCmd ex.Message

        if not started then
            failwithf "Failed to start process %s" lpCmd

    let SendBytesToPrinter (szPrinterName: string, data: byte[]) =
        use tmpFile = new TempFile()
        tmpFile.Write data
        sendToPrinter szPrinterName tmpFile.Path 