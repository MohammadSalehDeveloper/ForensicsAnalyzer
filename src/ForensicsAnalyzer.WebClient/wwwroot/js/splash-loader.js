import * as THREE from "../lib/three/three.min.js";

(function initSplashLoader() {
    const canvas = document.getElementById("splash-canvas");
    const app = document.getElementById("app");
    if (!canvas || !app || !canvas.isConnected) {
        return;
    }

    const stage = canvas.parentElement;
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(42, 1, 0.1, 100);
    camera.position.set(0, 0.35, 5.2);

    const renderer = new THREE.WebGLRenderer({
        canvas,
        antialias: true,
        alpha: true,
        powerPreference: "high-performance",
    });
    renderer.setClearColor(0x000000, 0);
    renderer.setPixelRatio(Math.min(window.devicePixelRatio || 1, 2));

    scene.add(new THREE.AmbientLight(0xb8c7e0, 0.55));
    const keyLight = new THREE.DirectionalLight(0xffffff, 1.15);
    keyLight.position.set(2.5, 3.5, 4);
    scene.add(keyLight);
    const fillLight = new THREE.DirectionalLight(0x5eead4, 0.35);
    fillLight.position.set(-3, -1, 2);
    scene.add(fillLight);

    const rings = [
        {
            radius: 1.85,
            tube: 0.035,
            color: 0x3d8bfd,
            rot: { x: 0.012, y: 0.018, z: 0.008 },
            tilt: { x: 1.05, y: 0.15, z: 0.08 },
        },
        {
            radius: 1.45,
            tube: 0.03,
            color: 0x5eead4,
            rot: { x: -0.016, y: 0.01, z: -0.014 },
            tilt: { x: 0.95, y: -0.22, z: 0.12 },
        },
        {
            radius: 1.1,
            tube: 0.025,
            color: 0xe8eef8,
            rot: { x: 0.01, y: -0.02, z: 0.012 },
            tilt: { x: 1.15, y: 0.08, z: -0.18 },
        },
    ].map((cfg) => {
        const geometry = new THREE.TorusGeometry(cfg.radius, cfg.tube, 16, 96);
        const material = new THREE.MeshStandardMaterial({
            color: cfg.color,
            metalness: 0.55,
            roughness: 0.28,
            emissive: cfg.color,
            emissiveIntensity: 0.18,
        });
        const mesh = new THREE.Mesh(geometry, material);
        mesh.rotation.set(cfg.tilt.x, cfg.tilt.y, cfg.tilt.z);
        mesh.userData.rot = cfg.rot;
        scene.add(mesh);
        return mesh;
    });

    let animationId = 0;
    let disposed = false;

    function resize() {
        if (disposed || !stage) {
            return;
        }

        const width = stage.clientWidth || 224;
        const height = stage.clientHeight || 224;
        camera.aspect = width / height;
        camera.updateProjectionMatrix();
        renderer.setSize(width, height, false);
    }

    function animate() {
        if (disposed) {
            return;
        }

        animationId = requestAnimationFrame(animate);
        for (const ring of rings) {
            const { rot } = ring.userData;
            ring.rotation.x += rot.x;
            ring.rotation.y += rot.y;
            ring.rotation.z += rot.z;
        }
        renderer.render(scene, camera);
    }

    function dispose() {
        if (disposed) {
            return;
        }

        disposed = true;
        cancelAnimationFrame(animationId);
        window.removeEventListener("resize", resize);
        observer.disconnect();

        for (const ring of rings) {
            ring.geometry.dispose();
            ring.material.dispose();
            scene.remove(ring);
        }

        renderer.dispose();
        if (renderer.forceContextLoss) {
            renderer.forceContextLoss();
        }
    }

    const observer = new MutationObserver(() => {
        if (!canvas.isConnected) {
            dispose();
        }
    });
    observer.observe(app, { childList: true, subtree: true });

    window.addEventListener("resize", resize);
    resize();
    animate();
})();
