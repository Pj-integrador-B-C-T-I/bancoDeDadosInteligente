"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { ArrowRightCircleIcon } from "@heroicons/react/24/solid";
import { toast } from "sonner";

export default function InputNewMessage() {
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const router = useRouter(); // hook do Next.js

  const handleSend = async () => {
    if (!input.trim()) return;

    setLoading(true);

    const token = localStorage.getItem("token");
    if (!token) {
      toast.error("Usuário não autenticado");
      setLoading(false);
      return;
    }

    try {
      const res = await fetch("http://localhost:8000/api/chat/semantic", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization":
            `Bearer ${token}`,
        },
        body: JSON.stringify({
          question: input,
          context: "",
        }),
      });

      if (!res.ok) {
        throw new Error(`Erro ${res.status}: ${res.statusText}`);
      }

      const data = await res.json();

      // ✅ Redireciona para a página do chat recém-criado
      router.push(`/Chat/${data.chat.id}`);
    } catch (err: any) {
      console.error(err);
      toast.error("❌ Erro ao enviar a pergunta.");
    } finally {
      setLoading(false);
      setInput("");
    }
  };

  return (
    <div className="flex flex-col items-center justify-center w-full bg-gray-100 gap-4">
      <div className="flex py-2 bg-[#D9D9D9] w-7/12 rounded-full items-center justify-between px-2 gap-2 mx-auto">
        <input
          type="text"
          placeholder="Pergunte alguma coisa..."
          className="px-5 h-12 w-full focus:outline-none bg-transparent"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleSend()}
          disabled={loading}
        />
        <button onClick={handleSend} disabled={loading}>
          <ArrowRightCircleIcon
            className={`h-16 w-16 ${
              loading ? "text-gray-400" : "text-gray-900"
            }`}
          />
        </button>
      </div>
    </div>

  );
}
