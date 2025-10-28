"use client";

import { poppins } from "@/app/fonts";
import ArticleCard from "@/components/ArticleCard";
import Navbar from "@/components/Navbar";

export default function ArticlePage() {

  const produtos = [
        {
        titulo: "Pizza de Barata ao Molho de Salsicha",
        descricao: "Pizza feita com crosta crocante de barata desidratada e molho especial de salsicha velha, com queijo fundido e presunto que passou do prazo de validade. ",
        },
        {
            titulo: "Suco de Meia Suada de Algodão",
            descricao: "Suco 100% natural feito com frutas frescas e uma pitada especial de meia suada de algodão.",
        },
        {
            titulo: "Sopa de Cabelo de Laranja",
            descricao: "Uma sopa cremosa feita com caldo de legumes e pedaços de cabelo de laranja, aquele toque gourmet que só os chefs mais ousados conhecem. ",
        },
        {
            titulo: "Hambúrguer de Pão de Queijo com Restos de Peixe",
            descricao: "Hambúrguer com pão de queijo caseiro e uma mistura de peixe do dia, combinado com o que sobrou do almoço do chef. ",
        },
        {
            titulo: "Sorvete de Maçã Podre com Granulado de Mofo",
            descricao: "Sorvete artesanal feito com maçãs cuidadosamente escolhidas (podres, é claro) e granulado de mofo envelhecido. ",
        },

    ]
  return (
    <div className={`${poppins.className} w-screen items-center justify-center flex flex-col gap-16`}>
      <Navbar />
      <div className="">
        <p className="text-3xl font-bold text-gray-400 text-center">
          Adicionar Novo Relatório
        </p>
      </div>

      <div className=" pb-2 flex gap-5 overflow-auto w-[80vw] bg-amber-300">
        <div className="pb-2 flex gap-5 overflow-x-auto overflow-y-hidden w-[80vw] bg-amber-300 scrollbar-hide">
                          
          {
            produtos.map((index,key)=>{
                return(
                    <div key={key}>
                        <ArticleCard />
                    </div>
                )
            })
          }
        </div>
          
      </div>
    </div>
  );
}
