module ConsoleUtils

open System.Text.RegularExpressions

let readChar () =
    let key = System.Console.ReadKey true

    key.KeyChar

let readInt () =
    let input = System.Console.ReadLine()

    match System.Int32.TryParse(input) with
    | (true, value) -> Some value 
    | _ -> None 

let readTime () =
    let input = System.Console.ReadLine()

    let match' = Regex.Match(input, "^([0-2][0-3]):?([0-5][0-9])$")
    
    if match'.Success then Some $"{match'.Groups[1]}:{match'.Groups[2]}"
    else None

let setFgColorToRed () = System.Console.ForegroundColor <- System.ConsoleColor.Red
let setFgColorToGreen () = System.Console.ForegroundColor <- System.ConsoleColor.Green
let resetFgColor () = System.Console.ResetColor()
