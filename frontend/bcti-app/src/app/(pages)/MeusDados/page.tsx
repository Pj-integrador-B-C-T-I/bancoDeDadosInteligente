"use client";

import React, { useEffect, useState } from "react";
import Navbar from "@/components/Navbar";
import InputField from "@/components/ui/InputField";

export default function MeusDados() {
  const [formData, setFormData] = useState({
    id: 0,
    nome: "",
    email: "",
    telefone: "",
    cpf: "",
    tipo: "",
    ativo: true,
  });

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [isEditing, setIsEditing] = useState(false);

  // PEGAR ID DO USUÁRIO (exemplo usando localStorage)
  const userId =
    typeof window !== "undefined" && localStorage.getItem("userId")
      ? localStorage.getItem("userId")
      : null;

  useEffect(() => {
    if (!userId) {
      console.error("ID do usuário não encontrado.");
      setLoading(false);
    }
  }, [userId]);

  // ===========================
  // GET /api/Usuarios/{id}
  // ===========================
  const fetchUser = async () => {
    try {
      if (!userId) throw new Error("ID do usuário não está definido.");

      const response = await fetch(
        `http://localhost:5184/api/Usuarios/${userId}`
      );

      if (!response.ok) throw new Error("Erro ao buscar usuário");

      const data = await response.json();
      setFormData({
        id: data.id,
        nome: data.nome,
        email: data.email,
        telefone: data.telefone,
        cpf: data.cpf,
        tipo: data.tipo,
        ativo: data.ativo,
      });

      setLoading(false);
    } catch (err) {
      console.error(err);
      setLoading(false);
    }
  };

  useEffect(() => {
    if (userId) fetchUser();
  }, [userId]);

  // ===========================
  // PUT /api/Usuarios/{id}
  // ===========================
  const updateUser = async () => {
    setSaving(true);

    try {
      const resp = await fetch(
        `http://localhost:5184/api/Usuarios/${formData.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            accept: "text/plain",
          },
          body: JSON.stringify({
            nome: formData.nome,
            telefone: formData.telefone,
            tipo: formData.tipo,
            ativo: formData.ativo,
          }),
        }
      );

      if (!resp.ok) throw new Error("Erro ao atualizar usuário");

      alert("Dados atualizados com sucesso!");
      setIsEditing(false); // Desabilitar campos após salvar
    } catch (err) {
      console.error(err);
      alert("Erro ao atualizar dados.");
    } finally {
      setSaving(false);
    }
  };

  const handleEdit = () => {
    setIsEditing(true);
  };

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };

  if (loading) return <p className="text-center mt-10">Carregando...</p>;

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
            disabled={!isEditing}
          />

          <InputField
            label="Email"
            type="email"
            value={formData.email}
            onChange={() => {}} // Adicionado para evitar erro de tipo
            disabled={true}
          />

          <InputField
            label="Telefone"
            value={formData.telefone}
            onChange={(e) => handleChange("telefone", e.target.value)}
            disabled={!isEditing}
          />

          <InputField
            label="CPF"
            value={formData.cpf}
            onChange={() => {}} // Adicionado para evitar erro de tipo
            disabled={true}
          />

          <InputField
            label="Cargo"
            value={formData.tipo}
            onChange={(e) => handleChange("tipo", e.target.value)}
            disabled={!isEditing}
          />

          <button
            type="button"
            onClick={isEditing ? updateUser : handleEdit}
            disabled={saving}
            className="bg-[#5680BA] hover:bg-[#5680BA]/90 text-white font-semibold rounded-md py-2 mt-2 cursor-pointer disabled:bg-gray-400"
          >
            {saving ? "SALVANDO..." : isEditing ? "SALVAR" : "EDITAR PERFIL"}
          </button>
        </form>
      </div>
    </div>
  );
}
