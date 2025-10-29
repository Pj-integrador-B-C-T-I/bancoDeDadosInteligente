"use client";

import { useState } from "react";
import { ArrowRightCircleIcon } from "@heroicons/react/24/solid";

interface InputNewMessageProps {
  onNewMessage: (question: string) => void;
}

export default function InputNewMessage({ onNewMessage }: InputNewMessageProps) {
  const [input, setInput] = useState("");

  const handleSend = () => {
    if (!input.trim()) return;

    onNewMessage(input); // envia a pergunta
    setInput("");
  };

  return (
    <div className="flex items-center justify-center w-screen bg-gray-100">
      <div className="flex py-2 bg-[#D9D9D9] w-7/12 rounded-full items-center justify-between px-2 gap-2">
        <input
          type="text"
          placeholder="Pergunte alguma coisa..."
          className="ml-5 px-2 h-12 w-11/12 focus:outline-none"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleSend()}
        />
        <button onClick={handleSend}>
          <ArrowRightCircleIcon className="h-16 w-16 text-gray-900" />
        </button>
      </div>
    </div>
  );
}
