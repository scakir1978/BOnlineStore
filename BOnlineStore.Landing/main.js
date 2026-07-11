/* ──────────────────────────────────────────
   B-Online Store ERP — Landing Page JS
   TR/EN i18n · Scroll animations · Navbar
   ────────────────────────────────────────── */

'use strict';

// ─── STATE ───────────────────────────────
let currentLang = localStorage.getItem('bos-lang') || 'tr';

// ─── INIT ─────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
  applyLanguage(currentLang);
  initNavbar();
  initHamburger();
  initLangToggle();
  initScrollAnimations();
  initSmoothScroll();
  initContactForm();
});

// ─── LANGUAGE ─────────────────────────────
function applyLanguage(lang) {
  currentLang = lang;
  localStorage.setItem('bos-lang', lang);

  // Update all elements with data-tr / data-en
  document.querySelectorAll('[data-tr]').forEach(el => {
    const text = lang === 'tr' ? el.dataset.tr : el.dataset.en;
    if (text !== undefined) el.textContent = text;
  });

  // Update input/textarea placeholders (data-ph-tr / data-ph-en)
  document.querySelectorAll('[data-ph-tr]').forEach(el => {
    el.placeholder = lang === 'tr' ? el.dataset.phTr : el.dataset.phEn;
  });

  // Update html lang attribute
  document.documentElement.lang = lang === 'tr' ? 'tr' : 'en';

  // Update toggle button labels
  const label = document.getElementById('langLabel');
  if (label) {
    const toggle = label.closest('.lang-toggle');
    if (toggle) {
      const active   = toggle.querySelector('.lang-active');
      const inactive = toggle.querySelector('.lang-inactive');
      if (lang === 'tr') {
        if (active)   active.textContent   = 'TR';
        if (inactive) inactive.textContent = 'EN';
      } else {
        if (active)   active.textContent   = 'EN';
        if (inactive) inactive.textContent = 'TR';
      }
    }
  }

  // Update page title & meta description
  const titles = {
    tr: 'B-Online Store ERP | Üretimi Formüle Edin',
    en: 'B-Online Store ERP | Formulate Your Production'
  };
  const descs = {
    tr: 'Pasta üretiminden metrekare hesabına — iç içe formülasyonlarla imalat yönetimi. Mamul→Yarı Mamul→Hammadde hiyerarşisi.',
    en: 'From pastry production to square meter calculation — manufacturing management with nested formulations. Finished Good→Semi-Finished→Raw Material hierarchy.'
  };
  document.title = titles[lang];
  const metaDesc = document.querySelector('meta[name="description"]');
  if (metaDesc) metaDesc.setAttribute('content', descs[lang]);
}

  // Update toggle button labels
  const label = document.getElementById('langLabel');
  if (label) {
    const toggle = label.closest('.lang-toggle');
    if (toggle) {
      const active   = toggle.querySelector('.lang-active');
      const inactive = toggle.querySelector('.lang-inactive');
      if (lang === 'tr') {
        if (active)   active.textContent   = 'TR';
        if (inactive) inactive.textContent = 'EN';
      } else {
        if (active)   active.textContent   = 'EN';
        if (inactive) inactive.textContent = 'TR';
      }
    }
  }

  // Update page title & meta description
  const titles = {
    tr: 'B-Online Store ERP | Üretimi Formüle Edin',
    en: 'B-Online Store ERP | Formulate Your Production'
  };
  const descs = {
    tr: 'Pasta üretiminden metrekare hesabına — iç içe formülasyonlarla imalat yönetimi. Mamul→Yarı Mamul→Hammadde hiyerarşisi.',
    en: 'From pastry production to square meter calculation — manufacturing management with nested formulations. Finished Good→Semi-Finished→Raw Material hierarchy.'
  };
  document.title = titles[lang];
  const metaDesc = document.querySelector('meta[name="description"]');
  if (metaDesc) metaDesc.setAttribute('content', descs[lang]);
}

function initLangToggle() {
  const btn = document.getElementById('langToggle');
  if (!btn) return;
  btn.addEventListener('click', () => {
    applyLanguage(currentLang === 'tr' ? 'en' : 'tr');
  });
}

// ─── NAVBAR ───────────────────────────────
function initNavbar() {
  const navbar = document.getElementById('navbar');
  if (!navbar) return;

  const onScroll = () => {
    navbar.classList.toggle('scrolled', window.scrollY > 20);
  };

  window.addEventListener('scroll', onScroll, { passive: true });
  onScroll();
}

// ─── HAMBURGER ────────────────────────────
function initHamburger() {
  const btn  = document.getElementById('hamburger');
  const menu = document.getElementById('mobileMenu');
  if (!btn || !menu) return;

  btn.addEventListener('click', () => {
    const isOpen = menu.classList.toggle('open');
    btn.setAttribute('aria-expanded', isOpen);
    // Animate hamburger → X
    const spans = btn.querySelectorAll('span');
    if (isOpen) {
      spans[0].style.cssText = 'transform:translateY(7px) rotate(45deg)';
      spans[1].style.cssText = 'opacity:0';
      spans[2].style.cssText = 'transform:translateY(-7px) rotate(-45deg)';
    } else {
      spans.forEach(s => s.style.cssText = '');
    }
  });

  // Close when a mobile link is clicked
  menu.querySelectorAll('.mobile-link').forEach(link => {
    link.addEventListener('click', () => {
      menu.classList.remove('open');
      btn.querySelectorAll('span').forEach(s => s.style.cssText = '');
    });
  });
}

// ─── SCROLL ANIMATIONS ────────────────────
function initScrollAnimations() {
  const targets = document.querySelectorAll('[data-animate]');
  if (!targets.length) return;

  const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry, i) => {
      if (entry.isIntersecting) {
        // Stagger children within the same parent
        setTimeout(() => {
          entry.target.classList.add('visible');
        }, (i % 4) * 100); // max 400ms stagger
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.12, rootMargin: '0px 0px -40px 0px' });

  targets.forEach(el => observer.observe(el));
}

// ─── SMOOTH SCROLL ────────────────────────
function initSmoothScroll() {
  document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', e => {
      const target = document.querySelector(anchor.getAttribute('href'));
      if (!target) return;
      e.preventDefault();
      const navHeight = document.getElementById('navbar')?.offsetHeight || 68;
      window.scrollTo({
        top: target.getBoundingClientRect().top + window.scrollY - navHeight - 12,
        behavior: 'smooth'
      });
    });
  });
}

// ─── CTA BUTTON TRACKING (optional) ───────
document.querySelectorAll('a[href*="ui.b-online-store.com"]').forEach(link => {
  link.addEventListener('click', () => {
    // Simple analytics hook — replace with GA/plausible if needed
    console.info('[BOS] CTA clicked → ui.b-online-store.com');
  });
});

// ─── CONTACT FORM ─────────────────────────
function initContactForm() {
  const form = document.getElementById('contactForm');
  if (!form) return;

  form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const submitBtn  = document.getElementById('formSubmitBtn');
    const idleState  = submitBtn.querySelector('.btn-submit-idle');
    const loadState  = submitBtn.querySelector('.btn-submit-loading');
    const successBox = document.getElementById('formSuccess');
    const errorBox   = document.getElementById('formError');

    // — Field values —
    const name    = document.getElementById('contactName').value.trim();
    const email   = document.getElementById('contactEmail').value.trim();
    const subject = document.getElementById('contactSubject').value;
    const message = document.getElementById('contactMessage').value.trim();

    // — Basic validation —
    if (!name || !email || !subject || !message) {
      // Trigger browser native validation UI
      form.reportValidity();
      return;
    }

    // — Loading state —
    submitBtn.disabled  = true;
    idleState.style.display  = 'none';
    loadState.style.display  = 'flex';
    successBox.style.display = 'none';
    errorBox.style.display   = 'none';

    try {
      // FormSubmit AJAX endpoint — first submission triggers email verification
      // to scakir1978@gmail.com (click the link once, then it works automatically)
      const res = await fetch('https://formsubmit.co/ajax/scakir1978@gmail.com', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept':       'application/json'
        },
        body: JSON.stringify({
          'Ad Soyad': name,
          'E-posta':  email,
          'Konu':     subject,
          'Mesaj':    message,
          _subject:   `[BOnlineStore ERP] ${subject} — ${name}`,
          _cc:        'mustafatoprak@gmail.com',
          _template:  'box',
          _captcha:   'false'
        })
      });

      const data = await res.json();

      if (data.success === 'true' || data.success === true) {
        successBox.style.display = 'flex';
        // Apply current language texts
        applyLanguage(currentLang);
        form.reset();
        // Scroll success message into view
        successBox.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
      } else {
        throw new Error('FormSubmit returned non-success');
      }
    } catch (err) {
      console.error('[BOS Contact]', err);
      errorBox.style.display = 'flex';
    } finally {
      submitBtn.disabled       = false;
      idleState.style.display  = 'flex';
      loadState.style.display  = 'none';
    }
  });
}

