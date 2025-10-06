"use client"

import ChatTag from "@/components/chat/chatTag";
import { usePathname } from "next/navigation";


export default function Sidebar() {

  const pathname = usePathname();

  const chats = [
    { id: "1", name: "Chat 1" },
    { id: "2", name: "Chat 2" },
    { id: "3", name: "Chat 3" },
  ];

  return (
    <div className="flex flex-col h-full w-2/12 bg-[#021837] items-center justify-start gap-10">

        <div className="flex flex-col gap-4 w-10/12">

            <h1 className="text-white text-3xl mt-10 font-bold pl-3">Chats</h1>

            {chats.map((chat) => (
                <ChatTag
                    key={chat.id}
                    content={chat.name}
                    current={pathname === `/Chat/${chat.id}`}
                    href={`/Chat/${chat.id}`}
                />

            ))}

        </div>
        
    </div>
  );
}