import { Loader2 } from "lucide-react";
import { Button } from "@/components/ui/button";

interface ButtonLoadingProps extends React.ComponentProps<typeof Button> {
  loading: boolean;
  children: React.ReactNode;
}

export function ButtonLoading({ loading, children, ...props }: ButtonLoadingProps) {
  return (
    <Button disabled={loading} {...props}>
      {loading && <Loader2 className="animate-spin mr-2 h-4 w-4" />}
      {loading ? "Por favor, espera" : children}
    </Button>
  );
}
