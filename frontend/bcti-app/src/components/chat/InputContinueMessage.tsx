"use client";

import { useState } from "react";
import { useParams } from "next/navigation";
import { ArrowRightCircleIcon } from "@heroicons/react/24/solid";
import LoadingCircle from "../shared/LoadingCircle";


interface InputContinueMessageProps {
  onNewMessage: (tempId: number, question: string, answer?: string) => void;
  onUpdateAnswer: (tempId: number, answer: string) => void;
}

export default function InputContinueMessage({ onNewMessage, onUpdateAnswer }: InputContinueMessageProps) {
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const params = useParams();
  const chatId = Number(params.id);

  const handleSend = async () => {
    if (!input.trim()) return;

    setLoading(true);

    const token = localStorage.getItem("token");
    if (!token) {
      alert("Usuário não autenticado");
      setLoading(false);
      return;
    }

    const tempId = Date.now(); // ID temporário para rastrear a mensagem

    // 1️⃣ Adiciona pergunta instantaneamente
    onNewMessage(tempId, input, "Carregando...");

    try {
      // 2️⃣ Envia para o backend
      const res = await fetch(`http://localhost:8000/api/chat/semantic/${chatId}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify({ question: input, context: "" }),
      });

      if (!res.ok) throw new Error(`Erro ${res.status}: ${res.statusText}`);

      const data = await res.json();

      // 3️⃣ Atualiza a resposta quando chegar
      onUpdateAnswer(tempId, data.answer_generated);

      // Limpa input
      setInput("");
    } catch (err: any) {
      console.error(err);
      onUpdateAnswer(tempId, "❌ Erro ao gerar resposta");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex flex-col items-center justify-center w-full bg-gray-100 gap-4">
      <div className="flex py-2 bg-[#D9D9D9] w-7/12 rounded-full items-center justify-between px-2 gap-2 mx-auto">
        <input
          type="text"
          placeholder="Continue a conversa..."
          className="px-5 h-12 w-full focus:outline-none bg-transparent"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleSend()}
          disabled={loading}
        />
        <button 
          onClick={handleSend} 
          disabled={loading}
          className="cursor-pointer"
        >
          {loading ? (
            <LoadingCircle />
          ) : (
            <ArrowRightCircleIcon
              className={`h-16 w-16 ${loading ? "text-gray-400" : "text-gray-900"}`}
            />
          )}
        </button>
      </div>

    </div>
  );
}
