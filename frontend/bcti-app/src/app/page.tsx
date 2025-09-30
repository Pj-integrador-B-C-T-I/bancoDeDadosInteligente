import Navbar from "@/components/NavbarTeste"
import { poppins } from "@/app/fonts";
import MiniChatBlock from "@/components/MiniChatBlock";
import ProductsSection from "@/components/ProductsSection";
import Footer from "@/components/Footer";
import Link from "next/link";

export default function HomePage() {
  return (
    <div className={`${poppins.className} flex flex-col text-black`} >

      <Navbar />
      <div className="flex flex-col h-screen mt-20">
        <div className="flex flex-row h-8/12 w-screen px-20 items-center justify-between mt-20">
          <div className="w-1/2 flex justify-center">
            <div className="text-black font-bold w-md text-6xl">
              <p>ACESSE A CT.IA E CHEGUE A RESOLUÇÃO DAS SUAS TAREFAS MAIS RÁPIDAS</p>
            </div>
          </div>
          <div className="w-1/2 flex justify-center">
            <Link href="/Chat">
              <MiniChatBlock />
            </Link>
          </div>
        </div>
        <div className="flex w-screen h-11/12 justify-center py-10 items-center mt-20 mb-20">
          <ProductsSection />
        </div>

        <Footer />
      </div>
    </div>
  )
}