"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import Navbar from "@/components/Navbar";
import Sidebar from "@/components/chat/Sidebar";
import InputNewMessage from "@/components/chat/inputNewMessage";
import ChatMessage from "@/components/chat/chatMessage";

interface Message {
  id: number;
  chatId: number;
  question: string;
  answer: string;
  createdAt: string;
}

export default function ChatPage() {
  const params = useParams();
  const chatId = Number(params.id);

  const [messages, setMessages] = useState<Message[]>([]);
  const [loading, setLoading] = useState(true);

  // Carregar mensagens do chat
  useEffect(() => {
    const fetchMessages = async () => {
      try {
        const res = await fetch(`http://localhost:5184/api/ChatMessage/chat/${chatId}`);
        if (!res.ok) throw new Error("Erro ao buscar mensagens");
        const data: Message[] = await res.json();
        setMessages(data);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    if (chatId) fetchMessages();
  }, [chatId]);

  // Envia nova pergunta e atualiza título do chat se for a primeira mensagem
  const handleNewMessage = async (question: string) => {
    const tempId = Date.now();

    // Adiciona mensagem temporária
    setMessages((prev) => [
      ...prev,
      { id: tempId, chatId, question, answer: "Carregando...", createdAt: new Date().toISOString() },
    ]);

    try {
      // Cria mensagem no backend
      const res = await fetch("http://localhost:5184/api/ChatMessage", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ chatId, question, answer: "Carregando..." }),
      });
      const data: Message = await res.json();

      // Atualiza mensagem temporária
      setMessages((prev) =>
        prev.map((m) => (m.id === tempId ? { ...m, id: data.id, answer: data.answer } : m))
      );

      // Se for a primeira mensagem do chat, atualiza o título do chat
      if (messages.length === 0) {
        const title = question.length > 20 ? question.slice(0, 20) + "..." : question;

        await fetch(`http://localhost:5184/api/Chat/${chatId}/title`, {
          method: "PATCH",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ title }),
        });

        // Opcional: atualizar Sidebar ou estado local de chats
      }
    } catch (err) {
      console.error(err);
      setMessages((prev) =>
        prev.map((m) => (m.id === tempId ? { ...m, answer: "Erro ao enviar" } : m))
      );
    }
  };

  return (
    <div className="w-screen h-screen flex flex-col">
      <Navbar />
      <div className="flex flex-row w-screen h-screen">
        <Sidebar />
        <div className="flex flex-col w-10/12">
          <div className="flex h-10/12 items-center justify-center overflow-y-auto">
            <div className="flex flex-col h-10/12 w-6/12 p-6 gap-3">
              {loading ? (
                <p>Carregando mensagens...</p>
              ) : messages.length === 0 ? (
                <p>Nenhuma mensagem encontrada</p>
              ) : (
                messages.map((m) => (
                  <div key={m.id} className="flex flex-col gap-1">
                    <ChatMessage message={m.question} isQuestion />
                    <ChatMessage message={m.answer} isQuestion={false} />
                  </div>
                ))
              )}
            </div>
          </div>

          <div className="flex h-2/12">
            <InputNewMessage onNewMessage={handleNewMessage} />
          </div>
        </div>
      </div>
    </div>
  );
}
