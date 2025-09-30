import { Geist, Geist_Mono, Lexend_Exa, Montserrat, Julius_Sans_One, Merriweather_Sans, Purple_Purse } from "next/font/google";

export const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

export const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const merriweatherSans = Merriweather_Sans({
  variable: "--font-merriweather-sans",
  subsets: ["latin"],
});

export const purplePurse = Purple_Purse({
  subsets: ["latin"],
  weight: "400",
  variable: "--font-purple-purse",
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
});