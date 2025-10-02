"use client";

import AuthLayout from "@/components/shared/AuthLayout";
import { Button } from "@/components/ui/button";
import {Input }from "@/components/ui/input";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "sonner";


export default function LoginPage() {

  const router = useRouter();
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const [redirecting, setRedirecting] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      setRedirecting(true); 
      
      const response = await fetch("http://localhost:5184/api/Auth/login", {
        method: "POST",
        headers: { 
          "accept": "/",
          "Content-Type": "application/json" 
        },
        body: JSON.stringify({ email, senha }),
      });

      const data = await response.json();

      if (response.ok) {
        localStorage.setItem("token", data.token);

        // Salvar o usuário inteiro (nome, email, ativo, etc)
        localStorage.setItem("user", JSON.stringify(data));

        toast.success("Login realizado com sucesso!", { duration: 2000 });

        setTimeout(() => {
          router.push("/");
        }, 2000);
      } else {
        toast.error(data.message || "Erro ao fazer login", { duration: 3000 });
        setRedirecting(false);
      }
    } catch (error) {
      console.error("Erro na conexão com o servidor:", error);
      toast.error("Erro na conexão com o servidor");
      setRedirecting(false); // Esconde o loader em caso de erro
    }
  };

  return (
    <AuthLayout>
        <div className="flex flex-col justify-between h-6/12 w-1/2">
            
            <h1 className="text-5xl font-bold w-4/5">Entre na sua conta</h1>
            <div>
              <form className="flex flex-col gap-2" onSubmit={handleSubmit}>
                <label htmlFor="Email">Email</label>
                <Input 
                  id="email"
                  type="email" 
                  placeholder="Email" 
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />

                <label htmlFor="Senha" className="mt-5">Senha</label>
                <Input
                  id="senha" 
                  type="password" 
                  placeholder="Senha" 
                  value={senha}
                  onChange={(e) => setSenha(e.target.value)}
                  required
                />
                <Button className="mt-5 bg-[#5680BA] hover:bg-[#5680BA]/90 cursor-pointer" type="submit">Entrar</Button>
              </form>

              <div className="flex flex-col gap-4 mt-7">
                <p className="flex items-center justify-end text-sm text-black">
                  Não tem uma conta?
                  <Link
                    href="/register"
                    className="ml-1 hover:underline"
                  >
                    Faça Cadastro
                  </Link>
                </p>
                <p className="flex items-center text-sm justify-end text-black">
                  Esqueceu a senha?
                  <Link
                    href="/forgot-password"
                    className="ml-1 hover:underline"
                  >
                    Clique aqui
                  </Link>
                </p>
              </div>
            </div>
            


        </div>
    </AuthLayout> 
  );
}
