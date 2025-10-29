"use client"

import ChatTag from "@/components/chat/chatTag";
import { usePathname, useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { Edit } from "lucide-react";

interface Chat {
  id: number;
  userId: number;
  title: string;
  createdAt: string;
}

export default function Sidebar() {
  const pathname = usePathname();
  const router = useRouter();
  const [chats, setChats] = useState<Chat[]>([]);
  const userId = 2; // substitua pelo ID do usuário autenticado

  // Carregar chats do usuário
  const fetchChats = async () => {
    try {
      const res = await fetch(`http://localhost:5184/api/Chat/user/${userId}`);
      if (!res.ok) throw new Error("Erro ao buscar chats");
      const data: Chat[] = await res.json();
      setChats(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchChats();
  }, [userId]);

  // Criar novo chat
  const handleNewChat = async () => {
    try {
      const res = await fetch("http://localhost:5184/api/Chat", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          userId,
          title: "Nova conversa" // título padrão
        }),
      });

      if (!res.ok) throw new Error("Erro ao criar chat");
      const data: Chat = await res.json();

      // Atualiza lista de chats
      setChats((prev) => [data, ...prev]);

      // Redireciona para o novo chat
      router.push(`/Chat/${data.id}`);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="flex flex-col h-full w-2/12 bg-[#021837] items-center justify-start gap-10">
      <div className="flex flex-col gap-4 w-10/12">
        <h1 className="text-white text-3xl mt-10 font-bold pl-3">Chats</h1>

        {/* Botão Novo Chat */}
        <div
          className="flex flex-row ml-3 items-center cursor-pointer hover:opacity-80 transition"
          onClick={handleNewChat}
        >
          <Edit className="h-6 w-6 text-white" />
          <h1 className="text-white text-xl font-bold pl-3">Novo Chat</h1>
        </div>

        {/* Lista de chats */}
        {chats.map((chat) => (
          <ChatTag
            key={chat.id}
            content={chat.title}
            current={pathname === `/Chat/${chat.id}`}
            href={`/Chat/${chat.id}`}
          />
        ))}
      </div>
    </div>
  );
}
