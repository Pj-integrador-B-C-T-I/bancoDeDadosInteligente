"use client";

import AuthLayout from "@/components/shared/AuthLayout";
import{ Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useSearchParams } from "next/navigation";
import { useState } from "react";
import { toast } from "sonner";

export default function RedefinirSenha() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const token = searchParams.get("token");

  const [senha, setSenha] = useState("");
  const [confirmarSenha, setConfirmarSenha] = useState("");

  const [redirecting, setRedirecting] = useState(false);
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (senha !== confirmarSenha) {
      toast.error("As senhas não coincidem");
      setRedirecting(false);
      return;
    }

    if (!token) {
      toast.error("Token de redefinição não encontrado.");
      return;
    }

    try {
      setRedirecting(true);
      const response = await fetch("http://localhost:5184/api/Auth/redefinir-senha", {
        method: "POST",
        headers: {
          accept: "/",
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ token, novaSenha: senha }),
      });

      const contentType = response.headers.get("content-type");
      let data;

      if (contentType && contentType.includes("application/json")) {
        data = await response.json();
      } else {
        data = await response.text();
      }

      if (response.ok) {
        toast.success("Senha redefinida com sucesso!", { duration: 2000 });
        setTimeout(() => {
          router.push("/Login");
        }, 2000);
      } else {
        toast.error(typeof data === "string" ? data : data?.message || "Erro ao redefinir senha", { duration: 3000 });
        setRedirecting(false);
      }
    } catch (error) {
      console.error("Erro na conexão com o servidor:", error);
      toast.error("Erro na conexão com o servidor");
      setRedirecting(false);
    }
  };
  return (
    <AuthLayout>
      <div className="flex w-1/2 flex-col gap-4">
         <h1 className="text-7xl font-bold mb-4"> Redefinir Senha </h1>
         <p className="mb-5 font-semibold"> Insira sua nova senha </p>
         
         <form onSubmit={handleSubmit} className="flex flex-col gap-4">
          <label htmlFor="senha">Nova senha</label>
          <Input
            type="password"
            id="senha"
            placeholder="Digite sua nova senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
            required
            
          />
         
          <label htmlFor="senha">Repita a senha</label>
          <Input
            type="password"
            id="senha"
            placeholder="Digite novamente sua senha"
            value={confirmarSenha}
            onChange={(e) => setConfirmarSenha(e.target.value)}
            required
          />
          <Button
          className="w-full mt-4 bg-[#5680ba] cursor-pointer hover:bg-[#5680ba]/90"
          type="submit">Redefinir Senha</Button>
         </form>
         <Link href="/Login">
          <p className="w-full justify-end flex hover:underline cursor-pointer pr-3">Voltar para o login</p>
         </Link>
        </div>
    </AuthLayout>
  );
}
