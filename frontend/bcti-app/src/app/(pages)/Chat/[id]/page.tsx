"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import Navbar from "@/components/Navbar";
import Sidebar from "@/components/chat/Sidebar";
import InputContinueMessage from "@/components/chat/InputContinueMessage";
import ChatMessage from "@/components/chat/chatMessage";
import LoaderDots from "@/components/shared/LoaderDots";

interface Message {
  id: number;
  question: string;
  answer: string;
}

interface ChatHistory {
  id: number;
  userId: number;
  title: string;
  createdAt: string;
  messages: Message[];
}

export default function ChatPage() {
  const params = useParams();
  const chatId = Number(params.id);

  const [messages, setMessages] = useState<Message[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchMessages = async () => {
      try {
        const res = await fetch(`http://localhost:5184/api/Chat/${chatId}/history`);
        if (!res.ok) throw new Error("Erro ao buscar histórico do chat");
        const data: ChatHistory = await res.json();
        setMessages(data.messages || []);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    if (chatId) fetchMessages();
  }, [chatId]);


  useEffect(() => {
    let interval: NodeJS.Timeout;

    const fetchMessages = async () => {
      try {
        const res = await fetch(`http://localhost:5184/api/Chat/${chatId}/history`);
        if (!res.ok) throw new Error("Erro ao buscar histórico do chat");
        const data: ChatHistory = await res.json();
        setMessages(data.messages || []);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    if (chatId) {
      fetchMessages(); // Busca inicial
      interval = setInterval(fetchMessages, 3000); // Busca a cada 3 segundos
    }

    return () => clearInterval(interval); // Limpa o intervalo ao desmontar
  }, [chatId]);

  return (
    <div className="w-screen h-screen flex flex-col overflow-y-hidden">
      <div className="fixed top-0 left-0 w-full z-50">
        <Navbar />
      </div>
      <div className="flex flex-row w-screen h-full pt-16">
        <Sidebar />
        
        {/* Área do chat */}
        <div className="flex flex-col w-10/12 relative h-full">
          {/* Mensagens roláveis */}
          <div className="flex-1 overflow-y-auto p-6">
            {loading ? (
              <p>Carregando mensagens...</p>
            ) : messages.length === 0 ? (
              <p>Nenhuma mensagem encontrada</p>
            ) : (
              messages.map((m, idx) => (
                <div key={idx} className="flex flex-col gap-1 mb-4">
                  <ChatMessage message={m.question} isQuestion />
                  {m.answer === "Resposta gerada automaticamente" ? (
                    // Loader no lugar da resposta
                    <div className="flex justify-start ml-4">
                      <LoaderDots />
                    </div>
                  ) : (
                    <ChatMessage message={m.answer} isQuestion={false} />
                  )}
                </div>
              ))
            )}
          </div>


          {/* Input fixado */}
          <div className="w-full bg-gray-100 p-4 ">
            <InputContinueMessage
              onNewMessage={(tempId, question, answer) => {
                setMessages(prev => [
                  ...prev,
                  { id: tempId, question, answer: answer || "" }
                ]);
              }}
              onUpdateAnswer={(tempId, answer) => {
                setMessages(prev =>
                  prev.map(m => (m.id === tempId ? { ...m, answer } : m))
                );
              }}
            />
          </div>
        </div>
      </div>
    </div>
  );
}
