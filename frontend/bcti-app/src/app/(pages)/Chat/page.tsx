"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Navbar from "@/components/Navbar";
import Sidebar from "@/components/chat/Sidebar";
import ChatMessage from "@/components/chat/chatMessage";
import { ArrowRightCircleIcon } from "@heroicons/react/24/solid";
import LoadingCircle from "@/components/shared/LoadingCircle";
import LoaderDots from "@/components/shared/LoaderDots";
import { toast } from "sonner";

interface Message {
  id: number;
  question: string;
  answer: string;
}

export default function NewChatPage() {
  const [messages, setMessages] = useState<Message[]>([]);
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const router = useRouter();

  const handleSend = async () => {
    if (!input.trim()) return;

    setLoading(true);

    const token = localStorage.getItem("token");
    if (!token) {
      toast.error("Usuário não autenticado");
      setLoading(false);
      return;
    }

    const tempId = Date.now();

    // Adiciona pergunta com resposta temporária
    setMessages((prev) => [
      ...prev,
      { id: tempId, question: input, answer: "Resposta gerada automaticamente" }
    ]);

    try {
      // Cria novo chat e envia a pergunta
      const res = await fetch("http://localhost:8000/api/chat/semantic", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify({
          question: input,
          context: "",
        }),
      });

      if (!res.ok) throw new Error(`Erro ${res.status}: ${res.statusText}`);
      const data = await res.json();

      // Atualiza a resposta
      setMessages((prev) =>
        prev.map((m) =>
          m.id === tempId ? { ...m, answer: data.answer_generated } : m
        )
      );

      // Redireciona para a página do chat recém-criado
      router.push(`/Chat/${data.chat.id}`);
    } catch (err) {
      console.error(err);
      setMessages((prev) =>
        prev.map((m) =>
          m.id === tempId ? { ...m, answer: "❌ Erro ao gerar resposta" } : m
        )
      );
    } finally {
      setLoading(false);
      setInput("");
    }
  };

  return (
    <div className="w-screen h-screen flex flex-col overflow-hidden">
      <div className="fixed top-0 left-0 w-full z-50">
        <Navbar />
      </div>
      <div className="flex flex-row w-screen h-full pt-16">
        <Sidebar />

        {/* Área do chat */}
        <div className="flex flex-col w-10/12 h-full">
          {/* Mensagens roláveis */}
          <div className="flex-1 overflow-y-auto p-6 flex flex-col gap-4">
            {messages.map((m) => (
              <div key={m.id} className="flex flex-col gap-1">
                <ChatMessage message={m.question} isQuestion />
                {m.answer === "Resposta gerada automaticamente" ? (
                  <div className="flex justify-start ml-4">
                    <LoaderDots />
                  </div>
                ) : (
                  <ChatMessage message={m.answer} isQuestion={false} />
                )}
              </div>
            ))}
          </div>

          {/* Input fixado */}
          <div className="w-full bg-gray-100 p-4">
            <div className="flex py-2 bg-[#D9D9D9] w-7/12 rounded-full items-center justify-between px-2 gap-2 mx-auto">
              <input
                type="text"
                placeholder="Pergunte algo para iniciar o chat..."
                className="px-5 h-12 w-full focus:outline-none bg-transparent"
                value={input}
                onChange={(e) => setInput(e.target.value)}
                onKeyDown={(e) => e.key === "Enter" && handleSend()}
                disabled={loading}
              />
              <button onClick={handleSend} disabled={loading}>
                {loading ? (
                  <LoadingCircle />
                ) : (
                  <ArrowRightCircleIcon className="h-16 w-16 text-gray-900" />
                )}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
