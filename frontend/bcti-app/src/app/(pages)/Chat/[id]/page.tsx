import Navbar from "@/components/Navbar";
import Sidebar from "@/components/chat/Sidebar";


export default function ChatPage() {
  return (
    <div className="w-screen h-screen">
        <Navbar />
        <Sidebar />
    </div>
    );
}