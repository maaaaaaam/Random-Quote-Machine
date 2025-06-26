const colors = [
    "#4A90E2", // Deep Blue
    "#F5A623", // Warm Orange
    "#89bab0", // Soft Mint
    "#B38DFF", // Gentle Lavender
    "#6A1B9A", // Deep Purple
    "#3A3A3A", // Graphite Grey
    "#c5b51a", // Sunny Yellow
    "#0099CC", // Mint Blue
    "#228B22", // Forest Green
    "#FF6F61"  // Light Coral
];

export function changeColor() {
    const index = Math.floor(Math.random() * colors.length);
    const color = colors[index];
    document.documentElement.style.setProperty('--color', color)
}

export async function getQuote() {

    //  fCC API fetching
    //
    // let arr;
    // try {
    //     await fetch('https://gist.githubusercontent.com/camperbot/5a022b72e96c4c9585c32bf6a75f62d9/raw/e3c6895ce42069f0ee7e991229064f167fe8ccdc/quotes.json')
    //         .then(res => res.json()).then(res => {
    //             arr = res.quotes
    //         })
    // } catch(err) {
    //     console.log('err:', err);
    // }
    // const index = Math.floor(Math.random() * (arr.length))
    // return {text: arr[index].quote, author: arr[index].author};


    //  Fullstack API fetching
    let quote
    try {
        await fetch('http://localhost:5142/api/quotes')
            .then(res => res.json()).then(res => {
                quote = res
            })
    } catch(err) {
        console.log('err:', err);
    }
    return quote;

}