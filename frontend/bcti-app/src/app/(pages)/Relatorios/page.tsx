"use client";

import { useEffect, useState } from "react";
import { poppins } from "@/app/fonts";
import ArticleCard from "@/components/ArticleCard";
import Navbar from "@/components/Navbar";
import FormRelatorio from "../cardRel/FormRelatorio";

interface Article {
  id: number;
  title: string;
  description: string;
  content?: string;
  authorId?: number;
  categoryId?: number;
  createdAt?: string;
  updatedAt?: string;
}

export default function ArticlePage() {
  const [artigos, setArtigos] = useState<Article[]>([]);
  const [mostrarForm, setMostrarForm] = useState(false);

  useEffect(() => {
    async function fetchArtigos() {
      try {
        const response = await fetch("http://localhost:5184/api/Article");
        if (!response.ok) {
          throw new Error("Erro ao buscar artigos");
        }
        const data: Article[] = await response.json();
        setArtigos(data);
      } catch (error) {
        console.error("Erro na requisição:", error);
      }
    }

    fetchArtigos();
  }, []);

  return (
    <div
      className={`${poppins.className} w-screen min-h-screen flex flex-col items-center bg-white overflow-x-hidden`}
    >
      <Navbar />

      <div className="w-full pl-4 mt-8">
        <h1 className="text-lg sm:text-xl pl-4 font-bold text-black">
          Relatórios Disponíveis
        </h1>
        <hr className="border-1 w-[20%] sm:w-[10%] border-black my-1" />
      </div>

      <div>
        <p
          className="text-xl sm:text-2xl font-bold text-gray-400 text-center mt-6 mb-6 cursor-pointer hover:text-gray-500 transition"
          onClick={() => setMostrarForm(true)}
        >
          Adicionar Novo Relatório
        </p>
      </div>

      <div
        className="flex flex-wrap justify-center gap-8 sm:gap-10 md:gap-12 
                   w-[90vw] sm:w-[85vw] lg:w-[80vw] pb-10"
      >
        {artigos.map((artigo) => (
          <ArticleCard
            key={artigo.id}
            title={artigo.title}
            description={artigo.description}
          />
        ))}
      </div>

      {/* === MODAL DO CARD - CORRIGIDO === */}
      {mostrarForm && (
        <div className="fixed inset-0 flex items-center justify-center bg-black/30 z-50 p-4">
          <div className="relative bg-white rounded-2xl shadow-xl w-full max-w-md">
            {/* Botão de fechar */}
            <button
              onClick={() => setMostrarForm(false)}
              className="absolute top-4 right-4 text-gray-500 hover:text-gray-700 text-xl font-bold z-10"
            >
              ×
            </button>
            
            {/* Container do formulário com padding */}
            <div className="p-6">
              <FormRelatorio />
            </div>
          </div>
        </div>
      )}
    </div>
  );
} 