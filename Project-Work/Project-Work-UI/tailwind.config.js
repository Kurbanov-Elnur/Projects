/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,js,ts,jsx,tsx}", "./public/index.html"],
  theme: {
    extend: {
      colors: {
        'custom-green': '#005d52',
      },
      scale: {
        '110': '1.13',
        '130': '1.3',
      },
    },
  },
  plugins: [],
};