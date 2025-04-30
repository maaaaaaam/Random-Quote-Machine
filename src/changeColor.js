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



export default function changeColor() {
    const index = Math.floor(Math.random() * colors.length);
    const color = colors[index];
    document.documentElement.style.setProperty('--color', color)
}