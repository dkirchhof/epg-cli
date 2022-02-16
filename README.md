# epg cli

Just a simple cli to show the german tv program.  
Under the hood, it uses a web crawler to get the data from tvtoday.  
  
Hint:  
As i'm not interested in all channels, it will only crawl the first 65 ones.
To get a list of all channels, checkout https://www.tvtoday.de/programm/tv-guide and call `[...document.querySelectorAll("select > optgroup > option")].map(e => e.innerText)` in the console.  
You can set the `maxChannel` in `src/Program.fs` to the last channel you are interested in. 

## dev
`dotnet watch run`

## build
`dotnet build --configuration Release`

## run
`dotnet run` or run the compiled program directly `bin/Release/net6.0/epg-cli`
