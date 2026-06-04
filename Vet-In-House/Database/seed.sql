-- ═══════════════════════════════════════════════════════════════
-- VET IN HOUSE — Datos de prueba (10 registros por tabla)
-- Ejecutar en: Supabase Dashboard → SQL Editor → New query → Run
-- IMPORTANTE: Ejecutar DESPUÉS de migration.sql
-- ═══════════════════════════════════════════════════════════════

-- UUIDs fijos para auth.users (usuarios)
-- a1..01 = admin | a1..02,03,04 = paseadores | a1..05-10 = clientes

-- ── 1. AUTH.USERS ────────────────────────────────────────────────
INSERT INTO auth.users (
  instance_id, id, aud, role, email, encrypted_password,
  email_confirmed_at, raw_app_meta_data, raw_user_meta_data,
  created_at, updated_at
) VALUES
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000001','authenticated','authenticated','admin@vetinhouse.co',         crypt('Admin123**',    gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000002','authenticated','authenticated','carlos.walker@gmail.com',      crypt('Carlos123**',   gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000003','authenticated','authenticated','maria.paseos@gmail.com',       crypt('Maria123**',    gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000004','authenticated','authenticated','andres.dogs@gmail.com',        crypt('Andres123**',   gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000005','authenticated','authenticated','sofia.martinez@gmail.com',     crypt('Sofia123**',    gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000006','authenticated','authenticated','diego.hernandez@gmail.com',    crypt('Diego123**',    gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000007','authenticated','authenticated','valentina.torres@gmail.com',   crypt('Vale123**',     gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000008','authenticated','authenticated','sebastian.ramirez@gmail.com',  crypt('Sebas123**',    gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000009','authenticated','authenticated','camila.vargas@gmail.com',      crypt('Camila123**',   gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now()),
  ('00000000-0000-0000-0000-000000000000','a1000000-0000-0000-0000-000000000010','authenticated','authenticated','felipe.castro@gmail.com',      crypt('Felipe123**',   gen_salt('bf')),now(),'{"provider":"email","providers":["email"]}','{}',now(),now());

-- ── 2. PROFILES ──────────────────────────────────────────────────
INSERT INTO public.profiles (id, user_id, nombre, email, telefono, rol) VALUES
  ('b1000000-0000-0000-0000-000000000001','a1000000-0000-0000-0000-000000000001','Juan Admin',         'admin@vetinhouse.co',        '3001234567','admin'),
  ('b1000000-0000-0000-0000-000000000002','a1000000-0000-0000-0000-000000000002','Carlos Rodríguez',   'carlos.walker@gmail.com',    '3112345678','paseador'),
  ('b1000000-0000-0000-0000-000000000003','a1000000-0000-0000-0000-000000000003','María González',     'maria.paseos@gmail.com',     '3123456789','paseador'),
  ('b1000000-0000-0000-0000-000000000004','a1000000-0000-0000-0000-000000000004','Andrés López',       'andres.dogs@gmail.com',      '3134567890','paseador'),
  ('b1000000-0000-0000-0000-000000000005','a1000000-0000-0000-0000-000000000005','Sofía Martínez',     'sofia.martinez@gmail.com',   '3145678901','cliente'),
  ('b1000000-0000-0000-0000-000000000006','a1000000-0000-0000-0000-000000000006','Diego Hernández',    'diego.hernandez@gmail.com',  '3156789012','cliente'),
  ('b1000000-0000-0000-0000-000000000007','a1000000-0000-0000-0000-000000000007','Valentina Torres',   'valentina.torres@gmail.com', '3167890123','cliente'),
  ('b1000000-0000-0000-0000-000000000008','a1000000-0000-0000-0000-000000000008','Sebastián Ramírez',  'sebastian.ramirez@gmail.com','3178901234','cliente'),
  ('b1000000-0000-0000-0000-000000000009','a1000000-0000-0000-0000-000000000009','Camila Vargas',      'camila.vargas@gmail.com',    '3189012345','cliente'),
  ('b1000000-0000-0000-0000-000000000010','a1000000-0000-0000-0000-000000000010','Felipe Castro',      'felipe.castro@gmail.com',    '3190123456','cliente');

-- ── 3. PASEADORES (los 3 usuarios con rol paseador) ──────────────
INSERT INTO public.paseadores (id, user_id, documento_url, biografia, estado_verificacion, disponible) VALUES
  ('e1000000-0000-0000-0000-000000000001','a1000000-0000-0000-0000-000000000002',
   'https://storage.example.com/docs/carlos-cc.pdf',
   'Soy Carlos, estudiante de veterinaria con 3 años paseando perros en Bogotá. Amo los animales y tengo experiencia con razas grandes.',
   'aprobado', true),
  ('e1000000-0000-0000-0000-000000000002','a1000000-0000-0000-0000-000000000003',
   'https://storage.example.com/docs/maria-cc.pdf',
   'Hola! Soy María, bióloga y apasionada por los perros. Ofrezco paseos seguros y llenos de diversión en el norte de Bogotá.',
   'aprobado', false),
  ('e1000000-0000-0000-0000-000000000003','a1000000-0000-0000-0000-000000000004',
   'https://storage.example.com/docs/andres-cc.pdf',
   'Andrés, entrenador canino certificado. Especializado en perros con ansiedad y necesidades especiales.',
   'pendiente', false);

-- ── 4. MASCOTAS ──────────────────────────────────────────────────
INSERT INTO public.mascotas (id, owner_id, nombre, raza, peso, edad, indicaciones_medicas) VALUES
  ('c1000000-0000-0000-0000-000000000001','a1000000-0000-0000-0000-000000000005','Max',       'Golden Retriever', 28.5, 4, 'Alérgico al pollo. Tomar agua cada 30 min en caminatas largas.'),
  ('c1000000-0000-0000-0000-000000000002','a1000000-0000-0000-0000-000000000005','Luna',      'Poodle Toy',        3.2, 2, 'Vacunas al día. Collar de pulgas activo.'),
  ('c1000000-0000-0000-0000-000000000003','a1000000-0000-0000-0000-000000000006','Rocky',     'Bulldog Francés',  11.0, 6, 'Braquicéfalo — no ejercicio intenso en calor. Máximo 20 min de paseo.'),
  ('c1000000-0000-0000-0000-000000000004','a1000000-0000-0000-0000-000000000006','Coco',      'Chihuahua',         2.1, 1, 'Muy nervioso con extraños. Presentar despacio.'),
  ('c1000000-0000-0000-0000-000000000005','a1000000-0000-0000-0000-000000000007','Thor',      'Labrador Negro',   32.0, 5, 'Displasia de cadera leve. Evitar superficies muy duras.'),
  ('c1000000-0000-0000-0000-000000000006','a1000000-0000-0000-0000-000000000007','Mia',       'Beagle',           12.5, 3, 'Tiende a escaparse. Collar GPS activado. Siempre correa.'),
  ('c1000000-0000-0000-0000-000000000007','a1000000-0000-0000-0000-000000000008','Simba',     'Pastor Alemán',    35.0, 7, 'En tratamiento artritis. Paseo suave 30 min máx.'),
  ('c1000000-0000-0000-0000-000000000008','a1000000-0000-0000-0000-000000000008','Nala',      'Shih Tzu',          5.8, 2, 'Pelo largo — cepillar después del paseo.'),
  ('c1000000-0000-0000-0000-000000000009','a1000000-0000-0000-0000-000000000009','Duke',      'Doberman',         38.0, 4, 'Muy obediente. Responde a comandos en español e inglés.'),
  ('c1000000-0000-0000-0000-000000000010','a1000000-0000-0000-0000-000000000010','Lola',      'Dachshund',         7.3, 3, 'Problema de espalda — no subir escaleras ni saltos.');

-- ── 5. TARIFAS (la básica ya existe, agregar más tipos) ──────────
INSERT INTO public.tarifas (tipo_servicio, precio_por_hora, precio_base, activa) VALUES
  ('paseo_grupal',   18000, 0,     true),
  ('paseo_premium',  35000, 5000,  true),
  ('paseo_nocturno', 40000, 10000, true);

-- ── 6. SERVICIOS ─────────────────────────────────────────────────
INSERT INTO public.servicios (id, cliente_id, paseador_id, mascota_id, horas, tarifa_por_hora, tarifa_total, estado, ubicacion_lat, ubicacion_lng, notas) VALUES
  ('d1000000-0000-0000-0000-000000000001','a1000000-0000-0000-0000-000000000005','a1000000-0000-0000-0000-000000000002','c1000000-0000-0000-0000-000000000001',1.0,25000,25000,'finalizado',    4.6097,-74.0817,'Por favor pasar por el parque El Virrey'),
  ('d1000000-0000-0000-0000-000000000002','a1000000-0000-0000-0000-000000000006','a1000000-0000-0000-0000-000000000002','c1000000-0000-0000-0000-000000000003',0.5,25000,12500,'finalizado',    4.6782,-74.0582,'Solo vuelta al manzana'),
  ('d1000000-0000-0000-0000-000000000003','a1000000-0000-0000-0000-000000000007','a1000000-0000-0000-0000-000000000003','c1000000-0000-0000-0000-000000000005',1.5,25000,37500,'finalizado',    4.7110,-74.0721,'Parque de la 93'),
  ('d1000000-0000-0000-0000-000000000004','a1000000-0000-0000-0000-000000000008','a1000000-0000-0000-0000-000000000003','c1000000-0000-0000-0000-000000000007',1.0,25000,25000,'finalizado',    4.6351,-74.0703,'Paseo suave por el barrio'),
  ('d1000000-0000-0000-0000-000000000005','a1000000-0000-0000-0000-000000000009','a1000000-0000-0000-0000-000000000002','c1000000-0000-0000-0000-000000000009',2.0,25000,50000,'en_progreso',   4.6533,-74.0836,'Parque Simón Bolívar ida y vuelta'),
  ('d1000000-0000-0000-0000-000000000006','a1000000-0000-0000-0000-000000000010','a1000000-0000-0000-0000-000000000003','c1000000-0000-0000-0000-000000000010',1.0,25000,25000,'aceptado',      4.6254,-74.1640,'Caminata suave Andes'),
  ('d1000000-0000-0000-0000-000000000007','a1000000-0000-0000-0000-000000000005',NULL,                                  'c1000000-0000-0000-0000-000000000002',1.0,25000,25000,'buscando',      4.7180,-74.0308,'Cerca al Centro Comercial Santafé'),
  ('d1000000-0000-0000-0000-000000000008','a1000000-0000-0000-0000-000000000006',NULL,                                  'c1000000-0000-0000-0000-000000000004',0.5,25000,12500,'buscando',      4.6609,-74.0546,'Chapinero central'),
  ('d1000000-0000-0000-0000-000000000009','a1000000-0000-0000-0000-000000000007','a1000000-0000-0000-0000-000000000002','c1000000-0000-0000-0000-000000000006',1.0,25000,25000,'cancelado',     4.6523,-74.0925,'Cancelado por lluvia'),
  ('d1000000-0000-0000-0000-000000000010','a1000000-0000-0000-0000-000000000008',NULL,                                  'c1000000-0000-0000-0000-000000000008',1.5,25000,37500,'buscando',      4.6012,-74.0657,'Zona Rosa');

-- ── 7. TRACKING (puntos del servicio en_progreso) ────────────────
INSERT INTO public.tracking (servicio_id, latitud, longitud, timestamp) VALUES
  ('d1000000-0000-0000-0000-000000000005', 4.6533, -74.0836, now() - interval '25 min'),
  ('d1000000-0000-0000-0000-000000000005', 4.6541, -74.0829, now() - interval '20 min'),
  ('d1000000-0000-0000-0000-000000000005', 4.6558, -74.0815, now() - interval '15 min'),
  ('d1000000-0000-0000-0000-000000000005', 4.6572, -74.0801, now() - interval '10 min'),
  ('d1000000-0000-0000-0000-000000000005', 4.6589, -74.0788, now() - interval '5 min'),
  ('d1000000-0000-0000-0000-000000000005', 4.6601, -74.0774, now() - interval '2 min'),
  ('d1000000-0000-0000-0000-000000000001', 4.6097, -74.0817, now() - interval '3 hours'),
  ('d1000000-0000-0000-0000-000000000001', 4.6105, -74.0809, now() - interval '2 hours 50 min'),
  ('d1000000-0000-0000-0000-000000000003', 4.7110, -74.0721, now() - interval '2 days'),
  ('d1000000-0000-0000-0000-000000000003', 4.7125, -74.0708, now() - interval '2 days');

-- ── 8. PRODUCTOS ─────────────────────────────────────────────────
INSERT INTO public.productos (id, nombre, descripcion, precio, categoria, stock) VALUES
  ('f1000000-0000-0000-0000-000000000001','Correa Retráctil 5m',        'Correa automática hasta 5m, ideal para parques. Máximo 25kg.',                         45000,'accesorios',30),
  ('f1000000-0000-0000-0000-000000000002','Arnés Antipull Talla M',     'Arnés ergonómico que reduce jalones. Talla M (10-20kg). Reflectivo.',                  62000,'accesorios',25),
  ('f1000000-0000-0000-0000-000000000003','Croquetas Royal Canin 3kg',  'Alimento seco premium para adultos medianos. Bolsa 3kg.',                              89000,'alimentacion',50),
  ('f1000000-0000-0000-0000-000000000004','Snack Dentales 20 unidades', 'Galletas limpiadoras de dientes. Sabor pollo. Pack x20.',                              28000,'alimentacion',80),
  ('f1000000-0000-0000-0000-000000000005','Cama Ortopédica Talla L',    'Cama con espuma ortopédica para perros grandes. Funda lavable.',                      135000,'descanso',15),
  ('f1000000-0000-0000-0000-000000000006','Juguete Kong Classic M',     'Juguete relleable caucho natural. Ideal para ansiedad. Talla M.',                      35000,'juguetes',40),
  ('f1000000-0000-0000-0000-000000000007','Shampoo Antipulgas 500ml',   'Shampoo con permetrina 0.05%. Elimina pulgas y garrapatas.',                           22000,'higiene',60),
  ('f1000000-0000-0000-0000-000000000008','GPS Collar Tracker',         'Rastreador GPS para collar. App Android/iOS. Batería 7 días.',                        189000,'tecnologia',10),
  ('f1000000-0000-0000-0000-000000000009','Comedero Automático 2.5L',   'Dispensador programable de comida. 2.5 litros. 4 comidas/día.',                       145000,'alimentacion',12),
  ('f1000000-0000-0000-0000-000000000010','Pañoleta Identificadora',    'Pañoleta con campo para nombre y teléfono del dueño. Tallas S/M/L.',                   18000,'accesorios',100);

-- ── 9. PEDIDOS ───────────────────────────────────────────────────
INSERT INTO public.pedidos (id, cliente_id, total, estado, direccion_envio) VALUES
  ('ab000000-0000-0000-0000-000000000001','a1000000-0000-0000-0000-000000000005', 134000,'entregado', 'Cra 15 # 88-64 Apto 302, Chapinero, Bogotá'),
  ('ab000000-0000-0000-0000-000000000002','a1000000-0000-0000-0000-000000000006', 189000,'enviado',   'Calle 100 # 14-36, Usaquén, Bogotá'),
  ('ab000000-0000-0000-0000-000000000003','a1000000-0000-0000-0000-000000000007', 224000,'pagado',    'Av 19 # 103-15, Suba, Bogotá'),
  ('ab000000-0000-0000-0000-000000000004','a1000000-0000-0000-0000-000000000008',  63000,'pendiente', 'Cra 7 # 32-16, La Candelaria, Bogotá'),
  ('ab000000-0000-0000-0000-000000000005','a1000000-0000-0000-0000-000000000009', 107000,'pendiente', 'Calle 127 # 20-40, Cedritos, Bogotá'),
  ('ab000000-0000-0000-0000-000000000006','a1000000-0000-0000-0000-000000000010', 351000,'pagado',    'Cra 50 # 72-10, Barrios Unidos, Bogotá'),
  ('ab000000-0000-0000-0000-000000000007','a1000000-0000-0000-0000-000000000005', 207000,'enviado',   'Cra 15 # 88-64 Apto 302, Chapinero, Bogotá'),
  ('ab000000-0000-0000-0000-000000000008','a1000000-0000-0000-0000-000000000006',  45000,'entregado', 'Calle 100 # 14-36, Usaquén, Bogotá'),
  ('ab000000-0000-0000-0000-000000000009','a1000000-0000-0000-0000-000000000007',  89000,'cancelado', 'Av 19 # 103-15, Suba, Bogotá'),
  ('ab000000-0000-0000-0000-000000000010','a1000000-0000-0000-0000-000000000008', 270000,'pendiente', 'Cra 7 # 32-16, La Candelaria, Bogotá');

-- ── 10. PEDIDO_ITEMS ─────────────────────────────────────────────
INSERT INTO public.pedido_items (pedido_id, producto_id, cantidad, precio_unitario) VALUES
  ('ab000000-0000-0000-0000-000000000001','f1000000-0000-0000-0000-000000000004',2,28000),  -- 2 snacks
  ('ab000000-0000-0000-0000-000000000001','f1000000-0000-0000-0000-000000000007',1,22000),  -- shampoo
  ('ab000000-0000-0000-0000-000000000001','f1000000-0000-0000-0000-000000000010',3,18000),  -- 3 pañoletas
  ('ab000000-0000-0000-0000-000000000002','f1000000-0000-0000-0000-000000000008',1,189000), -- GPS
  ('ab000000-0000-0000-0000-000000000003','f1000000-0000-0000-0000-000000000003',1,89000),  -- croquetas
  ('ab000000-0000-0000-0000-000000000003','f1000000-0000-0000-0000-000000000006',1,35000),  -- kong
  ('ab000000-0000-0000-0000-000000000003','f1000000-0000-0000-0000-000000000010',1,18000),  -- pañoleta
  ('ab000000-0000-0000-0000-000000000003','f1000000-0000-0000-0000-000000000007',1,22000),  -- shampoo
  ('ab000000-0000-0000-0000-000000000003','f1000000-0000-0000-0000-000000000004',2,28000),  -- snacks
  ('ab000000-0000-0000-0000-000000000004','f1000000-0000-0000-0000-000000000006',1,35000);  -- kong

-- ── PAGOS ────────────────────────────────────────────────────────
INSERT INTO public.pagos (servicio_id, pedido_id, monto, metodo, estado, referencia_externa) VALUES
  ('d1000000-0000-0000-0000-000000000001',NULL,25000,'nequi',    'completado','NEQ-2026-001234'),
  ('d1000000-0000-0000-0000-000000000002',NULL,12500,'pse',      'completado','PSE-2026-005678'),
  ('d1000000-0000-0000-0000-000000000003',NULL,37500,'tarjeta',  'completado','TRJ-2026-009012'),
  ('d1000000-0000-0000-0000-000000000004',NULL,25000,'daviplata','completado','DAV-2026-003456'),
  ('d1000000-0000-0000-0000-000000000005',NULL,50000,'nequi',    'pendiente', 'NEQ-2026-007890'),
  (NULL,'ab000000-0000-0000-0000-000000000001',134000,'tarjeta', 'completado','TRJ-2026-001111'),
  (NULL,'ab000000-0000-0000-0000-000000000002',189000,'pse',     'completado','PSE-2026-002222'),
  (NULL,'ab000000-0000-0000-0000-000000000003',224000,'tarjeta', 'completado','TRJ-2026-003333'),
  (NULL,'ab000000-0000-0000-0000-000000000006',351000,'nequi',   'completado','NEQ-2026-004444'),
  (NULL,'ab000000-0000-0000-0000-000000000007',207000,'pse',     'completado','PSE-2026-005555');
