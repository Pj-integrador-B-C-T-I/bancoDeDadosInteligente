import type { Metadata } from "next";
import { Geist, Geist_Mono, Lexend_Exa, Montserrat, Julius_Sans_One, Purple_Purse } from "next/font/google";
import "./globals.css";
import { Toaster } from 'sonner';


const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});


const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const lexendExa = Lexend_Exa({
  variable: "--font-lexend-exa",
  subsets: ["latin"],
});

export const juliusSansOne = Julius_Sans_One({
  variable: "--font-julius-sans-one",
  subsets: ["latin"],
  weight: "400",
});

export const montSerrat = Montserrat({
  subsets: ['latin'],
})

const metadata: Metadata = {
  title: "BCTI",
  description: "Banco de Conhecimento Técnico Inteligente",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
    <body className={
      `${geistSans.className} ${geistMono.className} ${lexendExa.className} ${juliusSansOne.className} ${montSerrat.className}`
    }>
      <div className="min-h-screen flex flex-col">      
        {children}
        <Toaster richColors position="top-right" theme="dark" />
        
      </div>
    </body>
    </html>
  );
}
