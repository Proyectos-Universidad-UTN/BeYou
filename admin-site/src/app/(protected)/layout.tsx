import WithLayout from "@/components/Layout/WithLayout";

export default function ProtectedLayout({ children }: { children: React.ReactNode }) {
  return <WithLayout>{children}</WithLayout>;
}
