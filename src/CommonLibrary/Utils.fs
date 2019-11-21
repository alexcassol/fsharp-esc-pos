namespace CommonLibrary

module Utils =
    open System
    
    let randomStr = 
        let chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789"
        let charsLen = chars.Length
        let random = Random()
        fun len -> 
            let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
            String(randomChars)