import { Facebook, Instagram, Twitter, Linkedin } from 'lucide-react';

export default function Footer() {
  return (
    <footer className="mt-20 bg-black text-white">
      <div className="flex flex-wrap justify-between px-8 py-10">
        <div className="w-1/4">
          <p className="font-bold mb-4">NAVEGAÇÃO</p>
          <LinkList links={[
            { label: 'Início', href: '/' },
            { label: 'Soluções', href: '/solucoes' },
            { label: 'Sobre a CTI', href: '/sobre' },
            { label: 'Contato', href: '/contato' },
            { label: 'Blog', href: '/blog' },
          ]} />
        </div>

        <div className="w-1/4">
          <p className="font-bold mb-4">LEGAL</p>
          <LinkList links={[
            { label: 'Política de Privacidade', href: '/privacidade' },
            { label: 'Termos de Uso', href: '/termos' },
            { label: 'Política de Cookies', href: '/cookies' },
            { label: 'Termos Corporativos', href: '/corporativo' },
          ]} />
        </div>

        <div className="w-1/4">
          <p className="font-bold mb-4">CONTATO</p>
          <p>Telefone: (11) 4053-2155 / (11) 2858-4075</p>
          <p>Endereço: Rua Salgado de Castro, 46, Diadema – SP</p>
          <p>Horário: Seg–Qui 8h às 18h | Sex 8h às 17h</p>
        </div>

        <div className="w-1/4">
          <p className="font-bold mb-4">SIGA A CTI</p>
          <div className="flex space-x-4 mb-4">
            <a href="https://www.facebook.com/CTIBrasil" target="_blank" rel="noopener noreferrer">
              <Facebook color="white" size={24} />
            </a>
            <a href="https://www.instagram.com/ctibrasil" target="_blank" rel="noopener noreferrer">
              <Instagram color="white" size={24} />
            </a>
            <a href="https://twitter.com/ctibrasil" target="_blank" rel="noopener noreferrer">
              <Twitter color="white" size={24} />
            </a>
            <a href="https://www.linkedin.com/company/cti-tecnologia-da-informacao" target="_blank" rel="noopener noreferrer">
              <Linkedin color="white" size={24} />
            </a>
          </div>
          <p className="text-sm">© {new Date().getFullYear()} CTI Brasil. Todos os direitos reservados.</p>
        </div>
      </div>
    </footer>
  );
}

// Componente auxiliar para listas de links
function LinkList({ links }: { links: { label: string; href: string }[] }) {
  return (
    <ul className="space-y-2">
      {links.map((l, i) => (
        <li key={i}>
          <a href={l.href} className="hover:underline">
            {l.label}
          </a>
        </li>
      ))}
    </ul>
  );
}
