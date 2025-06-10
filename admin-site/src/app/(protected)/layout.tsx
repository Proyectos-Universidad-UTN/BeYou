import Sidebar from "@/components/Sidebar/Sidebar";

export default function WithLayout({ children }: { children: React.ReactNode }) {
    return (
        <div>
            <Sidebar />
            {children}
        </div>
    )
}