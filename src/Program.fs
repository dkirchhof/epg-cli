open Types

let maxChannel = 64;

type Mode = ChooseChannel | ChooseTime | Now | Quit

let getCurrentDate () = System.DateTime.Now.ToString "yyMMdd"
let getCurrentTime () = System.DateTime.Now.ToString "HH:mm"

let rec chooseMode () =
    printfn "[c]: choose channel"
    printfn "[t]: choose time"
    printfn "[n]: now"
    printfn "[q]: quit"

    let c = ConsoleUtils.readChar() 

    match c with
    | 'c' -> ChooseChannel
    | 't' -> ChooseTime
    | 'n' -> Now
    | 'q' -> Quit
    | _ -> chooseMode()

let rec chooseChannel channels =
    printfn "insert channel number"

    channels |> List.iteri (fun i channel -> printfn $"[{i}]: {channel.name}")

    let number = ConsoleUtils.readInt()
    
    match number with
    | Some n when n >= 0 && n < channels.Length -> channels[n]
    | _ -> chooseChannel channels

let rec chooseTime () =
    printfn "insert time <hh:mm> | <hhmm>"
    
    let time = ConsoleUtils.readTime()

    match time with
    | Some t -> t
    | None -> chooseTime()

let printShowsOfChannel currentTime channel =
    channel.shows |> Seq.iter (fun show -> 
        if show.time < currentTime && show.today then 
            ConsoleUtils.setFgColorToRed()
        else 
            ConsoleUtils.setFgColorToGreen()

        printfn $"{show.time}: {show.name} ({show.description})"

        ConsoleUtils.resetFgColor()
    )

let findShowInTime time channel =
    channel.shows |> Seq.tryFindBack (fun show ->
        show.time <= time && show.today
    ) 

let printShowsOfTime time channels =
    channels |> List.iter (fun channel ->
        let show = findShowInTime time channel

        match show with
        | Some show' -> printfn $"{channel.name.PadRight(25, '_')} {show'.time}: {show'.name} ({show'.description})"
        | None -> printfn $"{channel.name.PadRight(25, '_')} -"
    )

let rec loop channels currentTime =
    let mode = chooseMode()

    match mode with
    | ChooseChannel ->
        let channel = chooseChannel channels 

        printfn $"{channel.name}"
        printShowsOfChannel currentTime channel
    | ChooseTime ->
        let time = chooseTime()
        
        printShowsOfTime time channels
    | Now ->
        printfn $"{currentTime}"
        printShowsOfTime currentTime channels
    | Quit ->
         System.Environment.Exit 0

    loop channels currentTime

let main () =
    printfn "loading data..."
    
    let currentDate = getCurrentDate()
    let currentTime = getCurrentTime()
    let channels = Crawler.load currentDate maxChannel

    loop channels currentTime

main()
