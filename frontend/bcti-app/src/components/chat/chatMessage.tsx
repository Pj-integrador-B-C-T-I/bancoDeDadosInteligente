"use client";

import { FaUserCircle } from "react-icons/fa";
import { Bot } from "lucide-react";

interface ChatMessageProps {
  message: string;
  isQuestion?: boolean; // true = pergunta (usuário), false = resposta (bot)
}

export default function ChatMessage({ message, isQuestion = false }: ChatMessageProps) {
  return (
    <div
      className={`flex w-full px-6 py-3 ${
        isQuestion ? "justify-end" : "justify-start"
      }`}
    >
      <div
        className={`flex items-end gap-2 max-w-[70%] ${
          isQuestion ? "flex-row-reverse" : "flex-row"
        }`}
      >
        

        {/* Balão da mensagem */}
        <div
          className={`px-4 py-3 rounded-2xl text-base break-words ${
            isQuestion
              ? "bg-[#0F52AF] text-white "
              : "bg-[#5680BA] text-gray-900"
          }`}
        >
          {message}
        </div>
      </div>
    </div>
  );
}
