import * as THREE from "../lib/three/three.min.js";

/** @type {WeakMap<HTMLCanvasElement, object>} */
const activeSessions = new WeakMap();
/** @type {object | null} */
let currentSession = null;

const MODES = {
    idle: "idle",
    map: "map",
    contacts: "contacts",
    thumbnail: "thumbnail",
};

const MODE_COLORS = {
    idle: 0x3d8bfd,
    map: 0x34d399,
    contacts: 0x60a5fa,
    thumbnail: 0xfbbf24,
};

/**
 * Forensic dashboard scene with hover reactions for Map / Contacts / Thumbnail.
 * @param {HTMLCanvasElement} canvas
 */
export function initCasesDashboard(canvas) {
    if (!(canvas instanceof HTMLCanvasElement)) {
        console.error("[cases-dashboard] canvas element required", canvas);
        throw new Error("cases-dashboard: canvas element required");
    }

    disposeSession(canvas);

    const stage = canvas.parentElement;
    const scene = new THREE.Scene();
    scene.fog = new THREE.FogExp2(0x0b1220, 0.04);

    const camera = new THREE.PerspectiveCamera(38, 1, 0.1, 100);
    const cameraHome = new THREE.Vector3(0.15, 2.6, 6.4);
    const lookHome = new THREE.Vector3(0, 0.15, 0);
    camera.position.copy(cameraHome);
    camera.lookAt(lookHome);

    const renderer = new THREE.WebGLRenderer({
        canvas,
        antialias: true,
        alpha: true,
        powerPreference: "high-performance",
    });
    renderer.setClearColor(0x000000, 0);
    renderer.setPixelRatio(Math.min(window.devicePixelRatio || 1, 2));

    scene.add(new THREE.AmbientLight(0x9fb6d9, 0.6));
    const key = new THREE.DirectionalLight(0xffffff, 1.2);
    key.position.set(4, 6, 3);
    scene.add(key);
    const accentLight = new THREE.PointLight(0x3d8bfd, 1.3, 16);
    accentLight.position.set(1.5, 1.5, 2);
    scene.add(accentLight);

    const root = new THREE.Group();
    root.position.set(0, -0.2, 0);
    scene.add(root);

    // ── Shared radar base ──────────────────────────────────────────────
    const disc = new THREE.Mesh(
        new THREE.CircleGeometry(2.35, 64),
        new THREE.MeshStandardMaterial({
            color: 0x102038,
            metalness: 0.2,
            roughness: 0.85,
            transparent: true,
            opacity: 0.55,
            side: THREE.DoubleSide,
        })
    );
    disc.rotation.x = -Math.PI / 2;
    root.add(disc);

    const radarRings = [0.8, 1.4, 2.0].map((radius, i) => {
        const mesh = new THREE.Mesh(
            new THREE.TorusGeometry(radius, 0.012, 8, 80),
            new THREE.MeshBasicMaterial({
                color: i === 1 ? 0x5eead4 : 0x3d8bfd,
                transparent: true,
                opacity: 0.5 - i * 0.08,
            })
        );
        mesh.rotation.x = Math.PI / 2;
        root.add(mesh);
        return mesh;
    });

    const sweepShape = new THREE.Shape();
    sweepShape.moveTo(0, 0);
    sweepShape.absarc(0, 0, 2.2, 0, Math.PI / 5, false);
    sweepShape.lineTo(0, 0);
    const sweep = new THREE.Mesh(
        new THREE.ShapeGeometry(sweepShape, 24),
        new THREE.MeshBasicMaterial({
            color: 0x5eead4,
            transparent: true,
            opacity: 0.28,
            side: THREE.DoubleSide,
            depthWrite: false,
        })
    );
    sweep.rotation.x = -Math.PI / 2;
    sweep.position.y = 0.02;
    root.add(sweep);

    const core = new THREE.Mesh(
        new THREE.IcosahedronGeometry(0.28, 1),
        new THREE.MeshStandardMaterial({
            color: 0x3d8bfd,
            emissive: 0x2563eb,
            emissiveIntensity: 0.7,
            metalness: 0.55,
            roughness: 0.25,
            flatShading: true,
        })
    );
    core.position.y = 0.35;
    root.add(core);

    const coreGlow = new THREE.Mesh(
        new THREE.SphereGeometry(0.42, 24, 24),
        new THREE.MeshBasicMaterial({
            color: 0x3d8bfd,
            transparent: true,
            opacity: 0.12,
            depthWrite: false,
        })
    );
    coreGlow.position.copy(core.position);
    root.add(coreGlow);

    const beam = new THREE.Mesh(
        new THREE.CylinderGeometry(0.035, 0.035, 2.4, 12, 1, true),
        new THREE.MeshBasicMaterial({
            color: 0x5eead4,
            transparent: true,
            opacity: 0.35,
            side: THREE.DoubleSide,
            depthWrite: false,
        })
    );
    beam.position.set(0, 1.1, 0);
    root.add(beam);

    // ── Mode groups (shown/emphasized on hover) ────────────────────────
    const mapGroup = new THREE.Group();
    mapGroup.visible = false;
    root.add(mapGroup);

    // Map grid
    const grid = new THREE.GridHelper(4.2, 12, 0x34d399, 0x14532d);
    grid.position.y = 0.04;
    mapGroup.add(grid);

    const mapPins = [];
    const pinSpots = [
        [-1.2, 0.9],
        [0.6, -1.1],
        [1.4, 0.5],
        [-0.4, -0.6],
        [0.2, 1.3],
    ];
    for (const [x, z] of pinSpots) {
        const pin = new THREE.Group();
        const stem = new THREE.Mesh(
            new THREE.CylinderGeometry(0.03, 0.03, 0.55, 8),
            new THREE.MeshStandardMaterial({
                color: 0x34d399,
                emissive: 0x059669,
                emissiveIntensity: 0.5,
            })
        );
        stem.position.y = 0.35;
        const head = new THREE.Mesh(
            new THREE.SphereGeometry(0.12, 14, 14),
            new THREE.MeshStandardMaterial({
                color: 0x6ee7b7,
                emissive: 0x34d399,
                emissiveIntensity: 0.65,
            })
        );
        head.position.y = 0.7;
        pin.add(stem, head);
        pin.position.set(x, 0, z);
        mapGroup.add(pin);
        mapPins.push(pin);
    }

    // Pulse rings for map
    const mapPulse = new THREE.Mesh(
        new THREE.RingGeometry(0.15, 0.28, 32),
        new THREE.MeshBasicMaterial({
            color: 0x34d399,
            transparent: true,
            opacity: 0.55,
            side: THREE.DoubleSide,
            depthWrite: false,
        })
    );
    mapPulse.rotation.x = -Math.PI / 2;
    mapPulse.position.y = 0.06;
    mapGroup.add(mapPulse);

    const contactsGroup = new THREE.Group();
    contactsGroup.visible = false;
    root.add(contactsGroup);

    const contactNodes = [];
    const contactCount = 10;
    const contactLinkPositions = new Float32Array(contactCount * 2 * 3);
    const contactLinkGeo = new THREE.BufferGeometry();
    contactLinkGeo.setAttribute("position", new THREE.BufferAttribute(contactLinkPositions, 3));
    const contactLinks = new THREE.LineSegments(
        contactLinkGeo,
        new THREE.LineBasicMaterial({ color: 0x60a5fa, transparent: true, opacity: 0.45 })
    );
    contactsGroup.add(contactLinks);

    for (let i = 0; i < contactCount; i++) {
        const mesh = new THREE.Mesh(
            new THREE.SphereGeometry(0.11 + (i % 3) * 0.02, 16, 16),
            new THREE.MeshStandardMaterial({
                color: i === 0 ? 0xffffff : 0x60a5fa,
                emissive: i === 0 ? 0x93c5fd : 0x2563eb,
                emissiveIntensity: 0.55,
                metalness: 0.3,
                roughness: 0.4,
            })
        );
        mesh.userData = {
            phase: (i / contactCount) * Math.PI * 2,
            radius: i === 0 ? 0 : 0.85 + (i % 4) * 0.28,
            height: i === 0 ? 0.9 : 0.45 + (i % 3) * 0.25,
            speed: 0.01 + (i % 5) * 0.003,
        };
        contactsGroup.add(mesh);
        contactNodes.push(mesh);
    }

    const thumbnailGroup = new THREE.Group();
    thumbnailGroup.visible = false;
    root.add(thumbnailGroup);

    const frames = [];
    for (let i = 0; i < 6; i++) {
        const frame = new THREE.Mesh(
            new THREE.PlaneGeometry(0.7, 0.5),
            new THREE.MeshStandardMaterial({
                color: 0x1e293b,
                emissive: 0xfbbf24,
                emissiveIntensity: 0.15,
                metalness: 0.2,
                roughness: 0.55,
                side: THREE.DoubleSide,
            })
        );
        const border = new THREE.Mesh(
            new THREE.PlaneGeometry(0.78, 0.58),
            new THREE.MeshBasicMaterial({
                color: 0xfbbf24,
                transparent: true,
                opacity: 0.55,
                side: THREE.DoubleSide,
            })
        );
        border.position.z = -0.01;
        const holder = new THREE.Group();
        holder.add(border, frame);
        holder.userData = {
            phase: (i / 6) * Math.PI * 2,
            radius: 1.35,
            baseY: 0.55 + (i % 3) * 0.2,
            spin: 0.01 + (i % 3) * 0.004,
        };
        thumbnailGroup.add(holder);
        frames.push(holder);
    }

    // Idle orbiting evidence nodes
    const idleNodes = [];
    for (let i = 0; i < 12; i++) {
        const color = [0x3d8bfd, 0x5eead4, 0xf8fafc, 0x60a5fa][i % 4];
        const mesh = new THREE.Mesh(
            new THREE.OctahedronGeometry(0.07 + (i % 3) * 0.02, 0),
            new THREE.MeshStandardMaterial({
                color,
                emissive: color,
                emissiveIntensity: 0.4,
                flatShading: true,
            })
        );
        mesh.userData = {
            orbit: 0.75 + (i % 5) * 0.28,
            speed: 0.008 + (i % 6) * 0.002,
            phase: (i / 12) * Math.PI * 2,
            bob: 0.12 + (i % 4) * 0.07,
            bobSpeed: 0.02 + (i % 5) * 0.004,
        };
        root.add(mesh);
        idleNodes.push(mesh);
    }

    let animationId = 0;
    let disposed = false;
    let time = 0;
    let mode = MODES.idle;
    let targetMode = MODES.idle;
    let modeBlend = 0; // 0 idle emphasis, 1 focused mode
    let energy = 1;

    const cameraTarget = cameraHome.clone();
    const lookTarget = lookHome.clone();

    function setFocus(nextMode) {
        const normalized = String(nextMode || MODES.idle).toLowerCase();
        targetMode = MODES[normalized] ? normalized : MODES.idle;
    }

    function clearFocus() {
        targetMode = MODES.idle;
    }

    function applyModeVisibility() {
        const focused = modeBlend > 0.15;
        mapGroup.visible = focused && mode === MODES.map;
        contactsGroup.visible = focused && mode === MODES.contacts;
        thumbnailGroup.visible = focused && mode === MODES.thumbnail;

        for (const node of idleNodes) {
            node.visible = mode === MODES.idle || modeBlend < 0.55;
        }
    }

    function resize() {
        if (disposed || !stage) {
            return;
        }

        const width = Math.max(stage.clientWidth || canvas.clientWidth || 1, 1);
        const height = Math.max(stage.clientHeight || canvas.clientHeight || 1, 1);
        camera.aspect = width / height;
        camera.updateProjectionMatrix();
        renderer.setSize(width, height, false);
        canvas.style.width = "100%";
        canvas.style.height = "100%";
    }

    function animate() {
        if (disposed) {
            return;
        }

        animationId = requestAnimationFrame(animate);
        time += 1;

        // Smooth mode transition
        if (targetMode !== mode && modeBlend < 0.05) {
            mode = targetMode;
        }
        const wantBlend = targetMode === MODES.idle ? 0 : 1;
        modeBlend += (wantBlend - modeBlend) * 0.08;
        if (targetMode !== MODES.idle) {
            mode = targetMode;
        }
        applyModeVisibility();

        energy += ((targetMode === MODES.idle ? 1 : 1.75) - energy) * 0.06;
        const accent = MODE_COLORS[mode] || MODE_COLORS.idle;
        accentLight.color.setHex(accent);
        accentLight.intensity = 1.1 + modeBlend * 0.9;
        core.material.emissive.setHex(accent);
        coreGlow.material.color.setHex(accent);
        sweep.material.color.setHex(mode === MODES.map ? 0x34d399 : mode === MODES.thumbnail ? 0xfbbf24 : 0x5eead4);

        // Camera reactions per mode
        if (mode === MODES.map) {
            cameraTarget.set(0.1, 4.2 - modeBlend * 0.4, 5.2);
            lookTarget.set(0, 0, 0);
        } else if (mode === MODES.contacts) {
            cameraTarget.set(2.2, 1.8, 5.4);
            lookTarget.set(0, 0.6, 0);
        } else if (mode === MODES.thumbnail) {
            cameraTarget.set(-0.4, 1.6, 5.8);
            lookTarget.set(0, 0.7, 0);
        } else {
            cameraTarget.copy(cameraHome);
            lookTarget.copy(lookHome);
        }
        camera.position.lerp(cameraTarget, 0.06);
        const currentLook = new THREE.Vector3().copy(lookHome).lerp(lookTarget, modeBlend);
        camera.lookAt(currentLook);

        const sweepSpeed = mode === MODES.map ? 0.045 : 0.022;
        sweep.rotation.z = time * sweepSpeed * energy;
        core.rotation.y += 0.012 * energy;
        core.rotation.x += 0.006 * energy;
        core.scale.setScalar(1 + modeBlend * 0.18);
        coreGlow.scale.setScalar(1 + Math.sin(time * 0.04) * 0.08 + modeBlend * 0.25);
        beam.material.opacity = 0.18 + Math.sin(time * 0.05) * 0.1 + modeBlend * 0.2;
        beam.visible = mode === MODES.idle || mode === MODES.thumbnail;

        for (let i = 0; i < radarRings.length; i++) {
            radarRings[i].material.opacity = 0.3 + Math.sin(time * 0.03 + i) * 0.1 + modeBlend * 0.15;
            if (mode === MODES.map) {
                radarRings[i].material.color.setHex(0x34d399);
            } else if (mode === MODES.contacts) {
                radarRings[i].material.color.setHex(0x60a5fa);
            } else if (mode === MODES.thumbnail) {
                radarRings[i].material.color.setHex(0xfbbf24);
            } else {
                radarRings[i].material.color.setHex(i === 1 ? 0x5eead4 : 0x3d8bfd);
            }
        }

        for (const node of idleNodes) {
            if (!node.visible) {
                continue;
            }
            const { orbit, speed, phase, bob, bobSpeed } = node.userData;
            const angle = phase + time * speed * energy;
            node.position.set(
                Math.cos(angle) * orbit,
                0.35 + Math.abs(Math.sin(time * bobSpeed + phase)) * bob,
                Math.sin(angle) * orbit
            );
            node.rotation.y += 0.02;
        }

        if (mapGroup.visible) {
            const pulse = 1 + (time % 50) / 50;
            mapPulse.scale.setScalar(pulse);
            mapPulse.material.opacity = 0.55 * (1 - (time % 50) / 50);
            for (let i = 0; i < mapPins.length; i++) {
                const pin = mapPins[i];
                pin.position.y = Math.sin(time * 0.05 + i) * 0.08;
                pin.scale.setScalar(1 + modeBlend * 0.15);
            }
            grid.rotation.y = Math.sin(time * 0.008) * 0.04;
        }

        if (contactsGroup.visible) {
            const positions = contactLinkGeo.attributes.position.array;
            for (let i = 0; i < contactNodes.length; i++) {
                const node = contactNodes[i];
                const { phase, radius, height, speed } = node.userData;
                if (radius === 0) {
                    node.position.set(0, height, 0);
                } else {
                    const angle = phase + time * speed;
                    node.position.set(
                        Math.cos(angle) * radius,
                        height + Math.sin(time * 0.03 + phase) * 0.08,
                        Math.sin(angle) * radius
                    );
                }
                node.scale.setScalar(1 + modeBlend * 0.2);

                const hub = contactNodes[0].position;
                const base = i * 6;
                positions[base] = hub.x;
                positions[base + 1] = hub.y;
                positions[base + 2] = hub.z;
                positions[base + 3] = node.position.x;
                positions[base + 4] = node.position.y;
                positions[base + 5] = node.position.z;
            }
            contactLinkGeo.attributes.position.needsUpdate = true;
            contactLinks.material.opacity = 0.25 + modeBlend * 0.4;
        }

        if (thumbnailGroup.visible) {
            for (const holder of frames) {
                const { phase, radius, baseY, spin } = holder.userData;
                const angle = phase + time * 0.012;
                holder.position.set(
                    Math.cos(angle) * radius,
                    baseY + Math.sin(time * 0.04 + phase) * 0.12,
                    Math.sin(angle) * radius
                );
                holder.lookAt(camera.position);
                holder.rotation.z = Math.sin(time * spin + phase) * 0.08;
                holder.scale.setScalar(0.95 + modeBlend * 0.25);
            }
        }

        root.rotation.y += mode === MODES.idle ? 0.0015 : 0.0006;
        renderer.render(scene, camera);
    }

    function dispose() {
        if (disposed) {
            return;
        }

        disposed = true;
        cancelAnimationFrame(animationId);
        window.removeEventListener("resize", onResize);

        root.traverse((obj) => {
            if (obj.geometry) {
                obj.geometry.dispose();
            }
            if (obj.material) {
                if (Array.isArray(obj.material)) {
                    obj.material.forEach((m) => m.dispose());
                } else {
                    obj.material.dispose();
                }
            }
        });

        renderer.dispose();
        if (renderer.forceContextLoss) {
            renderer.forceContextLoss();
        }

        activeSessions.delete(canvas);
        if (currentSession && currentSession.canvas === canvas) {
            currentSession = null;
        }
    }

    function onResize() {
        resize();
    }

    window.addEventListener("resize", onResize);
    requestAnimationFrame(() => {
        if (!disposed) {
            resize();
            animate();
        }
    });

    const session = { canvas, dispose, setFocus, clearFocus };
    activeSessions.set(canvas, session);
    currentSession = session;
    console.info("[cases-dashboard] interactive animation started");
    return session;
}

function disposeSession(canvas) {
    const existing = activeSessions.get(canvas);
    if (existing) {
        existing.dispose();
    }
}

export function disposeCasesDashboard(canvas) {
    disposeSession(canvas);
}

export function setDashboardFocus(mode) {
    if (currentSession) {
        currentSession.setFocus(mode);
    }
}

export function clearDashboardFocus() {
    if (currentSession) {
        currentSession.clearFocus();
    }
}
