'use client'; 
// import { LoginForm } from './LoginForm';
// import { Container, CssBaseline } from '@mui/material';
import { LoginForm } from "./LoginForm";

// export const LoginPage = () => {
//   return (
//     <Container
//       component="main"
//       maxWidth="sm"
//       sx={{
//         display: 'flex',
//         justifyContent: 'center',
//         alignItems: 'center',
//         minHeight: '100vh',
//       }}
//     >
//       <CssBaseline />
//       <LoginForm />
//     </Container>
//   );
// };

export default function LoginPage() {
    return (
        <div className="bg-muted flex min-h-svh flex-col items-center justify-center p-6 md:p-10">
            <div className="w-full max-w-sm md:max-w-3xl">
                <LoginForm />
            </div>
        </div>
    )
}