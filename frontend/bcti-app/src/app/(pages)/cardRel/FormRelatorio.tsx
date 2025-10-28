import React, { FC, FormEvent } from "react";

const FormRelatorio: FC = () => {
  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log("Formulário enviado!");
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
        />

        {/* Campo: Descrição */}
        <textarea
          placeholder="Descrição*"
          rows={3}
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-gray-700 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"
          required
        ></textarea>

        {/* Linha com Categoria e Complemento */}
        <div className="flex gap-3">
          <select
            className="w-1/2 border border-gray-300 rounded-md px-3 py-2 text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          >
            <option value="">Categoria*</option>
            <option value="Relatório">Relatório</option>
            <option value="Artigo">Artigo</option>
            <option value="Outro">Outro</option>
          </select>

          <input
            type="text"
            placeholder="Complemento"
            className="w-1/2 border border-gray-300 rounded-md px-3 py-2 text-gray-700 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        {/* Campo Upload */}
        <label
          htmlFor="upload"
          className="block border border-gray-300 rounded-md px-3 py-3 text-gray-400 text-sm text-center cursor-pointer hover:bg-gray-50 transition"
        >
          Upload de Arquivos
        </label>
        <input id="upload" type="file" className="hidden" />

        {/* Botão Adicionar */}
        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition font-medium"
        >
          Adicionar
        </button>
      </form>
    </div>
  );
};

export default FormRelatorio;
