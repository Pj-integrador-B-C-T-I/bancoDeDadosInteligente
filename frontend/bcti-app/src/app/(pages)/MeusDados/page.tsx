"use client";

import React, { useState } from "react";
import Navbar from "@/components/Navbar";
import InputField from "@/components/ui/InputField";

export default function MeusDados() {
  const [formData, setFormData] = useState({
    nome: "",
    email: "",
    telefone: "",
    cpf: "",
    cargo: "",
  });

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };

  return (
    <div className="flex flex-col min-h-screen bg-white">
      <Navbar />
      <div className="flex flex-col items-center mt-16">
        <div className="w-30 h-30 bg-gray-200 rounded-full flex items-center justify-center mb-4">
          <span className="text-4xl text-gray-600">👤</span>
        </div>

        <form className="flex flex-col w-95">
          <InputField
            label="Nome Completo"
            value={formData.nome}
            onChange={(e) => handleChange("nome", e.target.value)}
            placeholder="Digite seu nome completo"
          />
          <InputField
            label="Email"
            type="email"
            value={formData.email}
            onChange={(e) => handleChange("email", e.target.value)}
            placeholder="Digite seu email"
          />
          <InputField
            label="Telefone"
            value={formData.telefone}
            onChange={(e) => handleChange("telefone", e.target.value)}
            placeholder="(00) 00000-0000"
          />
          <InputField
            label="CPF"
            value={formData.cpf}
            onChange={(e) => handleChange("cpf", e.target.value)}
            placeholder="000.000.000-00"
          />
          <InputField
            label="Cargo"
            value={formData.cargo}
            onChange={(e) => handleChange("cargo", e.target.value)}
            placeholder="Digite seu cargo"
          />

          <button
            type="button"
            className="bg-[#5680BA] hover:bg-[#5680BA]/90 text-white font-semibold rounded-md py-2 mt-2 cursor-pointer"
          >
            EDITAR PERFIL 
          </button>
        </form>
      </div>
    </div>
  );
}
