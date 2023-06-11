import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { LoginForm } from "./components/LoginForm";
import { UserList } from "./components/UserList";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
      path: '/login',
      element: <LoginForm />
    },
    {
          path: '/users',
          element: <UserList />
        },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
