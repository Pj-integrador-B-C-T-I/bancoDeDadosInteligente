import IconBox from "@/components/home/icons/IconBox";

interface ProductBlockProps {
  icon: "chat" | "stars" | "inbox";
  title: string;
  description: string;
}

export default function ProductBlock({ icon, title, description }: ProductBlockProps) {
  return (
    <div>
      <div className="flex flex-col p-5 mx-15">
        <IconBox icon={icon} />

        <div className="flex flex-col">
          <div className="p-3">
            <h2 className="text-2xl font-semibold">{title}</h2>
          </div>
          <div className="p-3">
            <p className="text-xl font-normal">
              {description}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
