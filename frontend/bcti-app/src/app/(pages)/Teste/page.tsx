"use client"

import { poppins } from "@/app/fonts";
import MiniChatBlock from "@/components/home/chat/MiniChatBlock";
import Navbar from "@/components/Navbar";
import HomeText from "@/components/home/HomeText";
import Footer from "@/components/Footer";
import ProductSection from "@/components/home/product/ProductsSection";

export default function Teste() {
  return (
    <div className={`${poppins.className} w-screen flex flex-col gap-4`}>
      <Navbar />
      <div className="flex flex-col md:flex-row">
        <HomeText />
        <MiniChatBlock />
        
      </div>

      <ProductSection />
      <Footer />
    </div>
  )
}
