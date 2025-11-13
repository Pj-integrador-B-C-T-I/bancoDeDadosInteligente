"use client";

import AuthLayout from "@/components/shared/AuthLayout";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useState } from "react";
import { toast } from "sonner";
import { useRouter } from "next/navigation";
import Link from "next/link";



export default function EsqueciMinhaSenhaPage() {
  
  const router = useRouter();
  const [email, setEmail] = useState("");
  const [sending, setSending] = useState(false);
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try{
      setSending(true);
      // Lógica para enviar o email de redefinição de senha
      const response = await fetch('http://localhost:5184/api/Auth/esqueci-minha-senha', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email }),

      })
      let message;
      const contentType = response.headers.get("content-type");

      if (contentType && contentType.includes("application/json")) {
        const data = await response.json();
        message = data.message || "Resposta recebida.";
      } else {
        message = await response.text(); // pega o texto direto
      }


      if(response.ok){
        toast.success(message || 'Email de redefinição de senha enviado com sucesso!');
        setTimeout(() => {
          router.push('/Login');
        }, 3000);
    }
      else{
        toast.error(message || 'Erro ao enviar email de redefinição de senha.' , {duration: 3000});
        setSending(false);
      }
    } catch (error) {
      console.error('Erro ao enviar email de redefinição de senha:', error);
      toast.error('Erro ao enviar email de redefinição de senha.', {duration: 3000});
      setSending(false);
    }

  }

  return (
    <AuthLayout>
        <div className="flex w-1/2 flex-col gap-4">
          <h1 className="text-5xl font-bold mb-4">Esqueci minha senha?</h1>
          <p className="mb-4 font-semibold">Redefina a senha  em duas etapas.</p>
          <label htmlFor="email">Email</label>

          <form onSubmit={handleSubmit} >
            <Input 
              type="email" 
              id="email" 
              placeholder="example@ctia.com.br"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <Button 
              className="w-full mt-4 bg-[#5680ba] cursor-pointer hover:bg-[#5680BA]/90"
              type="submit"
              disabled={sending}
            >
              Enviar
            </Button>
          </form>
          <Link href="/Login">
            <p className="w-full justify-end flex hover:underline cursor-pointer pr-3">Voltar</p>
          </Link>
        </div>
    </AuthLayout>
  );
}
