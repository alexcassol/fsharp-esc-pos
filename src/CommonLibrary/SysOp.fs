﻿namespace CommonLibrary

module SysOp =
    open System

    type OS =
            | OSX            
            | Windows
            | Linux

    let getOS = 
            match int Environment.OSVersion.Platform with
            | 4 | 128 -> Linux
            | 6       -> OSX
            | _       -> Windows
