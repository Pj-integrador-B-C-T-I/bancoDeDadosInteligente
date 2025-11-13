"use client"

import ChatTag from "@/components/chat/chatTag";
import { usePathname, useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { Edit } from "lucide-react";
import Link from "next/link";

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

  const orderedChats = [...chats].sort((a, b) => {
    if (`/Chat/${a.id}` === pathname) return -1; // chat ativo vem primeiro
    if (`/Chat/${b.id}` === pathname) return 1;  // outro chat depois
    return 0; // mantém a ordem original para os demais
  });

  return (
    <div className="flex flex-col h-screen w-2/12 bg-[#021837] items-center justify-start">
      <div className="flex flex-col gap-4 w-10/12">
        <h1 className="text-white text-3xl mt-10 mb-5 font-bold pl-3">Chats</h1>

        {/* Botão Novo Chat */}
        <div
          className="flex flex-row ml-3 items-center cursor-pointer hover:opacity-80 transition mb-2"
        >
          <Link href="/Chat" className="flex flex-row items-center">
            <Edit className="h-5 w-5 text-white " />
            <h1 className="text-white text-lg font-bold pl-3 hover:underline">Novo Chat</h1>
          </Link>
          
        </div>
      </div>

      {/* Lista de chats — rolagem */}
      <div className="flex-1 w-full overflow-y-auto mt-4">
        <div className="flex flex-col gap-4 px-4 pb-6">
          {orderedChats.map((chat) => (
            <ChatTag
              key={chat.id}
              content={chat.title}
              current={pathname === `/Chat/${chat.id}`}
              href={`/Chat/${chat.id}`}
            />
          ))}
        </div>
      </div>
    </div>
  );
}
