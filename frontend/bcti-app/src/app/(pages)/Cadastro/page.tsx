"use client"
import AuthLayout from "@/components/shared/AuthLayout";
import {Input} from "@/components/ui/input";
import {Button} from "@/components/ui/button";
import Link from "next/link";
import { useState } from "react";
import { toast } from "sonner";
import { useRouter } from "next/navigation";

export default function CadastroPage() {
  const [nome, setNome] = useState("")
  const [email, setEmail] = useState("")
  const [telefone, setTelefone] = useState("")
  const [cpf, setCpf] = useState("")
  const [cargo, setCargo] = useState("")
  const [senha, setSenha] = useState("")
  const [confirmarSenha, setConfirmarSenha] = useState("")
  const router = useRouter()
  const [redirecting, setRedirecting] = useState(false)

  // Função para validar CPF
  function validarCPF(cpf: string) {
    cpf = cpf.replace(/\D/g, "");
    if (cpf.length !== 11 || /^([0-9])\1+$/.test(cpf)) return false;
    let soma = 0;
    for (let i = 0; i < 9; i++) soma += parseInt(cpf.charAt(i)) * (10 - i);
    let resto = soma % 11;
    let digito1 = resto < 2 ? 0 : 11 - resto;
    if (parseInt(cpf.charAt(9)) !== digito1) return false;
    soma = 0;
    for (let i = 0; i < 10; i++) soma += parseInt(cpf.charAt(i)) * (11 - i);
    resto = soma % 11;
    let digito2 = resto < 2 ? 0 : 11 - resto;
    if (parseInt(cpf.charAt(10)) !== digito2) return false;
    return true;
  }

  // Função para validar telefone (formato brasileiro)
  function validarTelefone(telefone: string) {
    const regex = /^(\(?\d{2}\)?\s?)?(\d{4,5}\-?\d{4})$/;
    return regex.test(telefone.replace(/\D/g, ""));
  }

const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (senha !== confirmarSenha) {
      toast.error("As senhas não coincidem");
      setRedirecting(false);
      return;
    }

    if (!validarCPF(cpf)) {
      toast.error("CPF inválido");
      return;
    }
    if (!validarTelefone(telefone)) {
      toast.error("Telefone inválido");
      return;
    }

    try {
      setRedirecting(true);

      const response = await fetch('http://localhost:5184/api/Auth/cadastrar', {
        method: "POST",
        headers: {
          accept: "*/*",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          nome,
          email,
          senha,
          telefone,
          cpf,
          tipo: cargo || "cliente",
        }),
      });

      const data = await response.json();

      if (response.ok) {
        toast.success("Cadastro realizado com sucesso!", { duration: 2000 });
        setTimeout(() => {
          router.push("/Login");
        }, 2000);
      } else {
        toast.error(data.message || "Erro ao cadastrar", { duration: 3000 });
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
        <div className="flex flex-col gap-3 w-2/4">
        <form onSubmit={handleSubmit}>
          <h1 className="text-4xl font-semibold gap-3 mb-5">Faça seu Cadastro</h1>
            <div className="space-y-2 flex flex-col">
              <label>Nome completo</label>
              <Input className="w-full" value={nome}
                onChange={(e) => setNome(e.target.value)}
                required />
            </div><br/>

            <div className="space-y-2 flex flex-col">
              <label>Email</label>
              <Input className="w-full" value={email}
                onChange={(e) => setEmail(e.target.value)}
                required/>
            </div><br/>

            <div className="space-y-2 flex flex-col">
              <label>Telefone</label>
              <Input className="w-full" value={telefone}
                onChange={(e) => setTelefone(e.target.value)}
                required/>
            </div><br/>

            <div className="space-y-2 flex flex-col">
              <label>CPF</label>
              <Input className="w-full" value={cpf}
                onChange={(e) => setCpf(e.target.value)}
                required/>
            </div><br/>

            <div className="space-y-2 flex flex-col">
              <label>Cargo</label>
              <Input className="w-full" value={cargo}
                onChange={(e) => setCargo(e.target.value)}
                required/>
            </div><br/>

            <div className="space-y-2 flex flex-col">
              <label>Senha</label>
              <Input type="password" className="w-full" value={senha}
                onChange={(e) => setSenha(e.target.value)}
                required/>
            </div><br/>

            <div className="gap-3 flex flex-col">
              <label>Confirme sua senha</label>

              <Input type="password"className="w-full" 
                value={confirmarSenha}
                onChange={(e) => setConfirmarSenha(e.target.value)}
                required 
              />
            </div>

            <Button 
              className="w-full h-9 mt-4 bg-[#5680BA] cursor-pointer hover:bg-[#5680BA]/80" 
              type="submit"
              disabled={redirecting}
            >
              {redirecting ? "Cadastrando..." : "Confirma"}
            </Button>
          </form>
          <div className="flex justify-between">
          <Link href="/Login">
            <p className="w-full flex cursor-pointer justify-end pr-3">Já tem uma conta</p>
          </Link> 
          <Link href="/esqueci-minha-senha">
            <p className="w-full flex cursor-pointer pr-3">Esqueceu a senha</p>
          </Link> 
          </div>  
        </div> 
    </AuthLayout>
  );
}