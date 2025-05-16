'use client'

import './globals.css'
import Navbar from '@/components/Navbar'
import Footer from '@/components/Footer'
import { Provider } from 'react-redux'
import store from '@/Store/store'
import { Toaster } from 'react-hot-toast'

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        <link
          href="https://unpkg.com/boxicons/css/boxicons.min.css"
          rel="stylesheet"
        />
      </head>
      <Provider store={store}>
        <header>
          <Navbar />
        </header>
        <body className='App'>
          <Toaster
            position='top-center'
            reverseOrder={false}
            toastOptions={{
              duration: 4000,
            }}>
          </Toaster>
          {children}
        </body>
        <footer>
          <Footer />
        </footer>
      </Provider>
    </html>
  );
}