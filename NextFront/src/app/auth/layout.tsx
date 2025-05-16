'use client'

import React from 'react'
import { Provider } from 'react-redux'
import store from '@/Store/store'
import { Toaster } from 'react-hot-toast'

export default function NavigationLayout({ children }: { children: React.ReactNode }) {
  return (
    <Provider store={store}>
      <main className="app">
        {children}
        <Toaster
          position="top-center"
          reverseOrder={false}
          toastOptions={{
            duration: 4000,
          }}
        />
      </main>
    </Provider>
  )
}