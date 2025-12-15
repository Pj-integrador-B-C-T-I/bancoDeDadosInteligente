"use client";

import React, { FC, FormEvent, useEffect, useState } from "react";
import { toast } from "sonner";

const FormRelatorio: FC = () => {
  const [categories, setCategories] = useState<{ id: number; name: string }[]>(
    []
  );

  const [loadingCategories, setLoadingCategories] = useState(true);
  const [saving, setSaving] = useState(false);

  const [form, setForm] = useState({
    title: "",
    description: "",
    content: "",
    categoryId: "",
  });

  const authorId =
    typeof window !== "undefined"
      ? Number(localStorage.getItem("userId"))
      : null;

  // ==================================
  // GET /api/Category
  // ==================================
  const fetchCategories = async () => {
    try {
      const res = await fetch("http://localhost:5184/api/Category");
      if (!res.ok) throw new Error("Erro ao carregar categorias");

      const data = await res.json();
      setCategories(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingCategories(false);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  // ==================================
  // Submit POST /api/Article
  // ==================================
  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!authorId) {
      toast.error("Autor não identificado!");
      return;
    }

    if (!form.categoryId) {
      toast.success("Selecione uma categoria!");
      return;
    }

    setSaving(true);

    try {
      const response = await fetch("http://localhost:5184/api/Article", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          accept: "*/*",
        },
        body: JSON.stringify({
          title: form.title,
          description: form.description,
          content: form.content,
          authorId: authorId,
          categoryId: Number(form.categoryId),
        }),
      });

      if (!response.ok) throw new Error("Erro ao criar artigo");

      const created = await response.json();
      console.log("Criado:", created);

      toast.success("Artigo/Relatório criado com sucesso!");

      setForm({
        title: "",
        description: "",
        content: "",
        categoryId: "",
      });
    } catch (err) {
      console.error(err);
      toast.error("Erro ao criar artigo");
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="bg-white p-6 rounded-2xl shadow-md w-[400px]">
      <h2 className="text-lg font-semibold mb-4 text-gray-800">
        Adicione o relatório ou artigos
      </h2>

      <form className="space-y-4" onSubmit={handleSubmit}>
        {/* Campo: Título */}
        <input
          type="text"
          placeholder="Título*"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-700 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
          required
          value={form.title}
          onChange={(e) => setForm({ ...form, title: e.target.value })}
        />

        {/* Campo: Descrição */}
        <textarea
          placeholder="Descrição*"
          rows={3}
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-700 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"
          required
          value={form.description}
          onChange={(e) => setForm({ ...form, description: e.target.value })}
        ></textarea>

        {/* Campo: Conteúdo */}
        <textarea
          placeholder="Conteúdo completo*"
          rows={5}
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-700 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"
          required
          value={form.content}
          onChange={(e) => setForm({ ...form, content: e.target.value })}
        ></textarea>

        {/* Categorias */}
        <div className="flex gap-3">
          <select
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
            value={form.categoryId}
            onChange={(e) =>
              setForm({ ...form, categoryId: e.target.value })
            }
          >
            <option value="">
              {loadingCategories ? "Carregando categorias..." : "Categoria*"}
            </option>

            {categories.map((cat) => (
              <option key={cat.id} value={cat.id}>
                {cat.name}
              </option>
            ))}
          </select>
        </div>

        {/* Botão Adicionar */}
        <button
          type="submit"
          disabled={saving}
          className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition font-medium disabled:bg-gray-400"
        >
          {saving ? "Enviando..." : "Adicionar"}
        </button>
      </form>
    </div>
  );
};

export default FormRelatorio;
